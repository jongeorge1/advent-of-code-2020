using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions.Day22
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] deckInputs = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var deck1Input = deckInputs[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
            var deck2Input = deckInputs[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);

            var deck1 = new Queue<int>(deck1Input);
            var deck2 = new Queue<int>(deck2Input);

            while (deck1.Count > 0 && deck2.Count > 0)
            {
                int player1 = deck1.Dequeue();
                int player2 = deck2.Dequeue();

                if (player1 > player2)
                {
                    deck1.Enqueue(player1);
                    deck1.Enqueue(player2);
                }
                else
                {
                    deck2.Enqueue(player2);
                    deck2.Enqueue(player1);
                }
            }

            Queue<int> winner = deck1.Count == 0 ? deck2 : deck1;
            int multiplier = winner.Count;
            int total = 0;

            while (winner.Count > 0)
            {
                total += winner.Dequeue() * multiplier--;
            }

            return total.ToString();
        }
    }
}
