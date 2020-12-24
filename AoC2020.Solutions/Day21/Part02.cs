namespace AoC2020.Solutions.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Food[] foods = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Food(x))
                .ToArray();

            string[] allergens = foods.SelectMany(food => food.Allergens).Distinct().ToArray();
            string[] ingredients = foods.SelectMany(food => food.Ingredients).Distinct().ToArray();

            string[] inertIngredients = GetInertIngredients(foods, allergens, ingredients);

            // Remove inert ingredients
            foreach (Food food in foods)
            {
                food.Ingredients = food.Ingredients.Where(i => !inertIngredients.Contains(i)).ToList();
            }

            var knownAllergens = new Dictionary<string, string>();

            while (knownAllergens.Count != allergens.Length)
            {
                // Remove known ingredients and allergens from foods.
                foreach (Food food in foods)
                {
                    food.Allergens = food.Allergens.Where(a => !knownAllergens.Keys.Contains(a)).ToList();
                    food.Ingredients = food.Ingredients.Where(a => !knownAllergens.Values.Contains(a)).ToList();
                }

                // Iterate the list again; for each unknown allergen, intersect all the foods that contain that allergen and see
                // if the resulting list contains a single entry.
                foreach (string allergen in allergens.Where(x => !knownAllergens.Keys.Contains(x)))
                {
                    IEnumerable<Food> foodsWithAllergen = foods
                        .Where(x => x.Allergens.Contains(allergen));
                    var potentialIngredients = foodsWithAllergen.Aggregate(
                        foodsWithAllergen.First().Ingredients.AsEnumerable(),
                        (acc, curr) => acc.Intersect(curr.Ingredients))
                        .ToList();

                    if (potentialIngredients.Count == 1)
                    {
                        knownAllergens.Add(allergen, potentialIngredients.First());
                    }
                }
            }

            return string.Join(",", knownAllergens.OrderBy(x => x.Key).Select(x => x.Value));
        }

        private static List<string> GetAllergenCandidates(string allergen, Food[] foods, string[] inertIngredients)
        {
            return foods.Where(x => x.Allergens.Contains(allergen))
                .SelectMany(x => x.Ingredients)
                .Where(x => !inertIngredients.Contains(x))
                .Distinct()
                .ToList();
        }

        private static Dictionary<string, List<string>> ConvertToAllergenCandidatesByIngredient(Dictionary<string, List<string>> ingredientCandidatesByAllergen)
        {
            return ingredientCandidatesByAllergen
                .SelectMany(x => x.Value)
                .Distinct()
                .ToDictionary(
                    x => x,
                    x => ingredientCandidatesByAllergen.Where(c => c.Value.Contains(x)).Select(c => c.Key).ToList());
        }

        private static string[] GetInertIngredients(Food[] foods, string[] allergens, string[] ingredients)
        {
            // We're going to build up a list of all the ingredients that are definitely not each allergen
            var ingredientsThatAreNotSpecificAllergens = new Dictionary<string, List<string>>();

            foreach (string allergen in allergens)
            {
                // Find foods that contain that allergen.
                IEnumerable<Food> foodsThatContainCurrentAllergen = foods
                    .Where(food => food.Allergens.Contains(allergen));

                // Now we need the ingredients from each food that aren't in all of the other foods we've identified
                ingredientsThatAreNotSpecificAllergens[allergen] = foodsThatContainCurrentAllergen.SelectMany(food => food.Ingredients)
                    .Distinct()
                    .Select(ingredient => (ingredient, count: foodsThatContainCurrentAllergen.Count(food => food.Ingredients.Contains(ingredient))))
                    .Where(x => x.count < foodsThatContainCurrentAllergen.Count())
                    .Select(x => x.ingredient)
                    .ToList();

                // We also need to add in any ingredients that are not present in any of the foods containing the named
                // allergen
                ingredientsThatAreNotSpecificAllergens[allergen].AddRange(
                    ingredients.Where(i => !foodsThatContainCurrentAllergen.Any(f => f.Ingredients.Contains(i))));
            }

            // Now we need the intersection of all the ingredients in our dictionary. These will be ingredients
            // that definitely aren't any allergen.
            return ingredientsThatAreNotSpecificAllergens.Values.Aggregate(
                ingredientsThatAreNotSpecificAllergens.Values.First().AsEnumerable(),
                (acc, ingredients) => acc.Intersect(ingredients))
                .Distinct()
                .ToArray();
        }

        [DebuggerDisplay("{OriginalInput}")]
        private class Food
        {
            public Food(string input)
            {
                this.OriginalInput = input;
                string[] components = input.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                this.Ingredients = components[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                this.Allergens = components[1].Split(new char[] { ' ', ',', ')' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
            }

            public string OriginalInput { get; }

            public List<string> Ingredients { get; set; }

            public List<string> Allergens { get; set; }
        }
    }
}
