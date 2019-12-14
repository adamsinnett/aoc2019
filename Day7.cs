using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019
{
    class Day7
    {
        public static void Execute(string input)
        {
            Console.WriteLine("Day Seven");

            var memory = input.Split(",").Select(int.Parse).ToArray();
            var phases = Enumerable.Range(0, 5).Permutations().ToList();
            var part1 = RunIntCodeComputersFromPermutations(memory, phases);
            Console.WriteLine("Part 1 = {0}", part1);            
            
            var phases2 = Enumerable.Range(5, 5).Permutations().ToList();
            var part2 = RunIntCodeComputersFromPermutations(memory, phases2);
            Console.WriteLine("Part 2 = {0}", part2);
        }

        private static int RunIntCodeComputersFromPermutations(int[] memory, List<IList<int>> phases)
        {
            var maxOutput = 0;
            foreach (var phase in phases)
            {
                IntCodeComputer[] computers = {
                    new IntCodeComputer(memory, NewQueueOf(phase[0])),
                    new IntCodeComputer(memory, NewQueueOf(phase[1])),
                    new IntCodeComputer(memory, NewQueueOf(phase[2])),
                    new IntCodeComputer(memory, NewQueueOf(phase[3])),
                    new IntCodeComputer(memory, NewQueueOf(phase[4])),
                };

                var output = 0;
                while (computers.Where(c => !c.halted).Any()) {
                    foreach (var computer in computers)
                    {
                        if (computer.output == -99)
                        {
                            continue;
                        }
                        computer.EnqueueInput(output);
                        computer.Compute();
                        output = computer.output;
                    }
                }


                var currentOutput = computers.Last().output;
                if (currentOutput > maxOutput)
                {
                    maxOutput = currentOutput;
                }
            }

            return maxOutput;
        }

        private static Queue<int> NewQueueOf(int phase)
        {
            var queue = new Queue<int>();
            queue.Enqueue(phase);
            return queue;
        }
    }
}
