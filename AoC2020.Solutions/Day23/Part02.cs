namespace AoC2020.Solutions.Day23
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // The input contains 9 cups, labelled 1 to 9. To make life easier, we're going to make the labels 0-based
            // so we can use modulo arithmetic and have easy access to the cups.
            int[] inputLabels = input.ToCharArray().Select(x => x - '1').ToArray();
            Cup[] cups = Enumerable.Range(0, 1000000).Select(x => new Cup { Label = x }).ToArray();

            // Join up the entries in the array.
            for (int i = 0; i < inputLabels.Length; i++)
            {
                // If it's the first, then its previous is the 1,000,000th cup.
                if (i == 0)
                {
                    cups[inputLabels[i]].Previous = cups[999999];
                    cups[999999].Next = cups[inputLabels[i]];
                }
                else
                {
                    // Otherwise, its previous is whatever's before it in the input list.
                    cups[inputLabels[i]].Previous = cups[inputLabels[i - 1]];
                }

                // Similarly for the last...
                if (i == 8)
                {
                    cups[inputLabels[i]].Next = cups[9];
                    cups[9].Previous = cups[inputLabels[i]];
                }
                else
                {
                    cups[inputLabels[i]].Next = cups[inputLabels[i + 1]];
                }
            }

            // Now sort the rest out.
            for (int i = 9; i < 1000000; i++)
            {
                if (i != 9)
                {
                    cups[i].Previous = cups[i - 1];
                }

                if (i != 999999)
                {
                    cups[i].Next = cups[i + 1];
                }
            }

            Cup current = cups[inputLabels[0]];

            for (int move = 0; move < 10000000; move++)
            {
                Cup[] pickedUpCups = new[]
                {
                    current.Next,
                    current.Next.Next,
                    current.Next.Next.Next,
                };

                // Remove these cups from the list.
                current.Next = pickedUpCups[2].Next;

                int destinationCupLabel = current.Label == 0 ? 999999 : current.Label - 1;

                while (pickedUpCups.Any(c => c.Label == destinationCupLabel))
                {
                    destinationCupLabel = destinationCupLabel == 0 ? 999999 : destinationCupLabel - 1;
                }

                Cup destination = cups[destinationCupLabel];

                // Insert the picked up cups clockwise of the destination.
                pickedUpCups[2].Next = destination.Next;
                destination.Next.Previous = pickedUpCups[2];

                destination.Next = pickedUpCups[0];
                pickedUpCups[0].Previous = destination;

                current = current.Next;
            }

            // Now we need the labels of the two cups clockwise from 1 (index 0)
            return (((long)cups[0].Next.Label + 1) * (cups[0].Next.Next.Label + 1)).ToString();
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
