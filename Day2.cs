using System;
using System.Linq;

namespace AoC2019
{
    class DayTwo
    {
        private const int AddCmd = 1;
        private const int MultCmd = 2;
        private const int StopCmd = 99;

        public static void getCodes(string input)
        {
            System.Console.WriteLine("Day Two");

            var memory = input.Split(",").Select(int.Parse).ToArray();

            System.Console.WriteLine("Part 1 is {0}", Compute(memory.ToArray(), 12, 2));
            // Naive!
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    var output = Compute(memory.ToArray(), i, j);

                    if (output == 19690720)
                    {
                        System.Console.WriteLine("Part 2 is {0}", 100 * i + j);
                    }
                }
            }

     

        }

        private static int Compute(int[] codes, int noun, int verb)
        {
            // if codes is length < 4 bummer!
            codes[1] = noun;
            codes[2] = verb;

            int instructionPtr = 0;
            while (codes[instructionPtr] != StopCmd)
            {
                var cmd = codes[instructionPtr];
                var first = codes[instructionPtr + 1];
                var second = codes[instructionPtr + 2];
                var target = codes[instructionPtr + 3];

                switch (cmd)
                {
                    case AddCmd:
                        codes[target] = codes[first] + codes[second];
                        break;
                    case MultCmd:
                        codes[target] = codes[first] * codes[second];
                        break;
                    default:
                        throw new NotImplementedException();
                }

                instructionPtr = instructionPtr + 4;
            }

            return codes[0];
        }
    }
}
