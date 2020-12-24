namespace AoC2020.Solutions.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        private static readonly int[] Primes = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541 };

        public string Solve(string input)
        {
            string[] deckInputs = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<int>? deck1Input = deckInputs[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
            IEnumerable<int>? deck2Input = deckInputs[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);

            var deck1 = new Queue<int>(deck1Input);
            var deck2 = new Queue<int>(deck2Input);

            int winnerIndex = this.PlayRecursiveCombat(0, deck1, deck2);

            var winner = winnerIndex == 1 ? deck1 : deck2;

            return ScoreDeck(winner).ToString();
        }

        private int PlayRecursiveCombat(int depth, Queue<int> deck1, Queue<int> deck2)
        {
            int rounds = 0;
            var seenConfigurations = new Dictionary<string, bool>();

            while (deck1.Count != 0 && deck2.Count != 0)
            {
                rounds++;

                // Check if we've seen the current configuration of hands before in this game.
                string hash = GetDeckHashString(deck1, deck2);

                if (seenConfigurations.ContainsKey(hash))
                {
                    // Player 1 wins the game.
                    return 1;
                }

                seenConfigurations.Add(hash, true);

                int player1 = deck1.Dequeue();
                int player2 = deck2.Dequeue();
                int roundWinner = 0;

                if (deck1.Count >= player1 && deck2.Count >= player2)
                {
                    // Recursive combat time!
                    roundWinner = this.PlayRecursiveCombat(
                        depth + 1,
                        new Queue<int>(deck1.Take(player1)),
                        new Queue<int>(deck2.Take(player2)));
                }
                else
                {
                    // One of the players can't recurse so the round is decided on card value
                    roundWinner = player1 > player2 ? 1 : 2;
                }

                if (roundWinner == 1)
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

            return deck1.Count == 0 ? 2 : 1;
        }

        private static long GetDeckHash(IEnumerable<int> deck)
        {
            int index = 0;
            long hash = 0;

            foreach (int curr in deck)
            {
                hash += curr * Primes[index++];
            }

            return hash;
        }

        private static string GetDeckHashString(IEnumerable<int> deck1, IEnumerable<int> deck2)
        {
            return string.Concat(
                string.Join(",", deck1),
                "|",
                string.Join(",", deck2));
        }

        private static int ScoreDeck(Queue<int> deck)
        {
            int multiplier = deck.Count;
            int total = 0;

            while (deck.Count > 0)
            {
                total += deck.Dequeue() * multiplier--;
            }

            return total;
        }
    }
}
