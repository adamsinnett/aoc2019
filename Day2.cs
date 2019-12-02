using System;
using System.Linq;

namespace AoC2019
{
    class DayTwo
    {
        private const string input = @"1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,9,19,23,2,23,13,27,1,27,9,31,2,31,6,35,1,5,35,39,1,10,39,43,2,43,6,47,1,10,47,51,2,6,51,55,1,5,55,59,1,59,9,63,1,13,63,67,2,6,67,71,1,5,71,75,2,6,75,79,2,79,6,83,1,13,83,87,1,9,87,91,1,9,91,95,1,5,95,99,1,5,99,103,2,13,103,107,1,6,107,111,1,9,111,115,2,6,115,119,1,13,119,123,1,123,6,127,1,127,5,131,2,10,131,135,2,135,10,139,1,13,139,143,1,10,143,147,1,2,147,151,1,6,151,0,99,2,14,0,0";
        private const int AddCmd = 1;
        private const int MultCmd = 2;
        private const int StopCmd = 99;

        public static void getCodes()
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
