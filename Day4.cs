using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019
{
    class Day4
    {
        public static void DoDay4(string input)
        {
            System.Console.WriteLine("Day Four");
            var parsedInput = input.Split("-").Select(int.Parse).ToArray();
            var min = parsedInput[0];
            var max = parsedInput[1];

            var part1 = Enumerable.Range(min, max - min).Where(n => ValidPassword(n, false)).Count();
            var part2 = Enumerable.Range(min, max - min).Where(n => ValidPassword(n, true)).Count();
            System.Console.WriteLine("Part 1 is {0}", part1);
            System.Console.WriteLine("Part 2 is {0}", part2);

        }

        private static bool ValidPassword(int arg, bool preventTriples)
        {
            var hasDouble = false;
            var digits = arg.ToString().Select(o => Convert.ToInt32(o)).ToArray();
            for (int i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] > digits[i])
                {
                    return false;
                }
                if (digits[i - 1] == digits[i] && (!preventTriples || (i <= 1 || digits[i-2] != digits[i]) && (i>=5 || digits[i+1] != digits[i])))
                {
                    
                    hasDouble = true;
                }
            }
            return hasDouble;
        }
    }
}
