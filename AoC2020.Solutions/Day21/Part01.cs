namespace AoC2020.Solutions.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Food[] foods = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Food(x))
                .ToArray();

            string[] allergens = foods.SelectMany(food => food.Allergens).Distinct().ToArray();
            string[] ingredients = foods.SelectMany(food => food.Ingredients).Distinct().ToArray();

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
            string[] ingredientsThatAreNotAllergens = ingredientsThatAreNotSpecificAllergens.Values.Aggregate(
                ingredientsThatAreNotSpecificAllergens.Values.First().AsEnumerable(),
                (acc, ingredients) => acc.Intersect(ingredients))
                .Distinct()
                .ToArray();

            // Now find out how many foods each of these ingredients is in.
            return ingredientsThatAreNotAllergens.Sum(i => foods.Count(food => food.Ingredients.Contains(i))).ToString();
        }

        [DebuggerDisplay("{OriginalInput}")]
        private class Food
        {
            public Food(string input)
            {
                this.OriginalInput = input;
                string[] components = input.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                this.Ingredients = components[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                this.Allergens = components[1].Split(new char[] { ' ', ',', ')' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
            }

            public string OriginalInput { get; }

            public string[] Ingredients { get; }

            public string[] Allergens { get; }
        }
    }
}
