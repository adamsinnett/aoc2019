using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019
{
    class Day5
    {
    
        public static void Execute(string input)
        {
            System.Console.WriteLine("Day five");
            var memory = input.Split(",").Select(int.Parse).ToArray();

            var part1 = Compute(memory.ToArray(), 1);
            Console.WriteLine("Part 1 is {0}", part1);
            var part2 = Compute(memory.ToArray(), 5);
            Console.WriteLine("Part 1 is {0}", part2);

        }

        private static int Compute(int[] memory, int input)
        {
            var output = 0;
            var instPtr = 0;
            while (instPtr < memory.Length && memory[instPtr] != 99)
            {

                var cmd = memory[instPtr] % 100;
                switch (cmd)
                {
                    case 1:
                        {
                            var noun = getInstValue(memory, instPtr, true);
                            var verb = getInstValue(memory, instPtr, false);
                            memory[memory[instPtr + 3]] = noun + verb;
                            instPtr += 4;
                            break;
                        }
                    case 2:
                        {
                            var noun = getInstValue(memory, instPtr, true);
                            var verb = getInstValue(memory, instPtr, false);
                            memory[memory[instPtr + 3]] = noun * verb;
                            instPtr += 4;
                            break;
                        }

                    case 3:
                        {
                            memory[memory[instPtr + 1]] = input;
                            instPtr += 2;
                            break;
                        }

                    case 4:
                        {
                            output = getInstValue(memory, instPtr, true);
                            instPtr += 2;
                            break;
                        }

                    case 5:
                        {
                            var noun = getInstValue(memory, instPtr, true);
                            var verb = getInstValue(memory, instPtr, false);
                            instPtr = noun == 0 ? instPtr + 3 : verb;
                            break;
                        }

                    case 6:
                        {
                            var noun = getInstValue(memory, instPtr, true);
                            var verb = getInstValue(memory, instPtr, false);
                            instPtr = noun != 0 ? instPtr + 3 : verb;
                            break;
                        }

                    case 7:
                        {
                            var noun = getInstValue(memory, instPtr, true);
                            var verb = getInstValue(memory, instPtr, false);
                            memory[memory[instPtr + 3]] = noun < verb ? 1 : 0;
                            instPtr += 4;
                            break;
                        }

                    case 8:
                        {
                            var noun = getInstValue(memory, instPtr, true);
                            var verb = getInstValue(memory, instPtr, false);
                            memory[memory[instPtr + 3]] = noun == verb ? 1 : 0;
                            instPtr += 4;
                            break;
                        }
                }
            }

            return output;
        }

        private static int getInstValue(int[] memory, int instrPtr, bool isNounParam)
        {
            int mode = isNounParam ?  (memory[instrPtr]  / 100) % 10 : memory[instrPtr] / 1000;
            int offset = isNounParam ? 1 : 2;
            return mode != 0 ? memory[instrPtr + offset] : memory[memory[instrPtr + offset]];
        }
    }
}
