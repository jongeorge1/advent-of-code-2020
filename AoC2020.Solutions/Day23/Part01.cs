namespace AoC2020.Solutions.Day23
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            // The input contains 9 cups, labelled 1 to 9. To make life easier, we're going to make the labels 0-based
            // so we can use modulo arithmetic and have easy access to the cups.
            int[] inputLabels = input.ToCharArray().Select(x => x - '1').ToArray();
            Cup[] cups = inputLabels.Select(x => new Cup { Label = x }).ToArray();

            // Join it all up.
            for (int i = 0; i < cups.Length; i++)
            {
                int previousIndex = (i + cups.Length - 1) % cups.Length;
                int nextIndex = (i + 1) % cups.Length;

                cups[i].Previous = cups[previousIndex];
                cups[i].Next = cups[nextIndex];
            }

            Cup current = cups[0];

            // Now sort the cups so that they appear in order in the array above. This will make accessing them
            // much simpler.
            cups = cups.OrderBy(x => x.Label).ToArray();

            for (int move = 0; move < 100; move++)
            {
                Cup[] pickedUpCups = new[]
                {
                    current.Next,
                    current.Next.Next,
                    current.Next.Next.Next,
                };

                // Remove these cups from the list.
                current.Next = pickedUpCups[2].Next;

                int destinationCupLabel = current.Label == 0 ? 8 : current.Label - 1;

                while (pickedUpCups.Any(c => c.Label == destinationCupLabel))
                {
                    destinationCupLabel = destinationCupLabel == 0 ? 8 : destinationCupLabel - 1;
                }

                Cup destination = cups[destinationCupLabel];

                // Insert the picked up cups clockwise of the destination.
                pickedUpCups[2].Next = destination.Next;
                destination.Next.Previous = pickedUpCups[2];

                destination.Next = pickedUpCups[0];
                pickedUpCups[0].Previous = destination;

                current = current.Next;
            }

            return string.Concat(
                cups[0].Next.Label + 1,
                cups[0].Next.Next.Label + 1,
                cups[0].Next.Next.Next.Label + 1,
                cups[0].Next.Next.Next.Next.Label + 1,
                cups[0].Next.Next.Next.Next.Next.Label + 1,
                cups[0].Next.Next.Next.Next.Next.Next.Label + 1,
                cups[0].Next.Next.Next.Next.Next.Next.Next.Label + 1,
                cups[0].Next.Next.Next.Next.Next.Next.Next.Next.Label + 1);
        }

        [DebuggerDisplay("{Previous.Label + 1} <- {Label + 1} -> {Next.Label + 1}")]
        private class Cup
        {
            public int Label { get; set; }

#nullable disable
            public Cup Previous { get; set; }
#nullable enable

#nullable disable
            public Cup Next { get; set; }
#nullable enable
        }
    }
}
