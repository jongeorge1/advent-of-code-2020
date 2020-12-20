namespace AoC2020.Solutions.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] equations = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            long total = 0;

            foreach (string current in equations)
            {
                total += Evaluate(current);
            }

            return total.ToString();
        }

        private static long Evaluate(string equation)
        {
            // Evaluates the expression using the Shunting Yard Algorithm which I read about here:
            // https://cp-algorithms.com/string/expression_parsing.html
            // Although that page talks about applying the algorithm to expressions in reverse Polish notation, it
            // works fine for expressions written in the normal way.
            var terms = new Stack<long>();
            var operators = new Stack<char>();

            for (int index = 0; index < equation.Length; index++)
            {
                if (equation[index] == ' ')
                {
                    continue;
                }

                if (equation[index] == '(')
                {
                    operators.Push('(');
                }
                else if (equation[index] == ')')
                {
                    // Process back to the previous opening bracket...
                    while (operators.Peek() != '(')
                    {
                        ProcessOperator(terms, operators.Pop());
                    }

                    // Now pop the opening bracket as well
                    operators.Pop();
                }
                else if (equation[index] == '*' || equation[index] == '+')
                {
                    // The normal shunting yard algorithm addresses operator precedence here
                    // If there's already an operator on the stack and it's a higher than or equal priority to the one
                    // we've just encountered, then process the operator stack until that's no longer true, then put
                    // this new operator on the stack. This is how we deal with operator precedence; in our case, we
                    // don't really have any operator precedence, but we do need to take account of the possibility of
                    // brackets on the stack, which we need to stop processing at.
                    while (operators.Count != 0 && GetPrecedence(operators.Peek()) > GetPrecedence(equation[index]))
                    {
                        ProcessOperator(terms, operators.Pop());
                    }

                    operators.Push(equation[index]);
                }
                else
                {
                    // We're on a number
                    // 0 = 48, 1 = 49... 9 - 57
                    int number = (int)equation[index] - '0';
                    ++index;
                    while (index < equation.Length && equation[index] >= '0' && equation[index] <= '9')
                    {
                        number = (number * 10) + equation[index++];
                    }

                    // Rewind index 1 place to ensure we don't miss anything.
                    --index;

                    terms.Push(number);
                }
            }

            // Finish things up...
            while (operators.Count != 0)
            {
                ProcessOperator(terms, operators.Pop());
            }

            return terms.Pop();
        }

        private static int GetPrecedence(char op)
        {
            return op switch
            {
                '+' => 2,
                '*' => 1,
                _ => -1,
            };
        }

        private static void ProcessOperator(Stack<long> stack, char op)
        {
            long right = stack.Pop();
            long left = stack.Pop();

            if (op == '+')
            {
                stack.Push(left + right);
            }
            else
            {
                stack.Push(left * right);
            }
        }
    }
}
