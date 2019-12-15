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

            var memory = input.Split(",").Select(long.Parse).ToArray();
            var phases = Enumerable.Range(0, 5).Permutations().ToList();
            var part1 = RunIntCodeComputersFromPermutations(memory, phases);
            Console.WriteLine("Part 1 = {0}", part1);            
            
            var phases2 = Enumerable.Range(5, 5).Permutations().ToList();
            var part2 = RunIntCodeComputersFromPermutations(memory, phases2);
            Console.WriteLine("Part 2 = {0}", part2);
        }

        private static long RunIntCodeComputersFromPermutations(long[] memory, List<IList<int>> phases)
        {
            var maxOutput = 0L;
            foreach (var phase in phases)
            {
                IntCodeComputer[] computers = {
                    new IntCodeComputer(memory, NewQueueOf(phase[0])),
                    new IntCodeComputer(memory, NewQueueOf(phase[1])),
                    new IntCodeComputer(memory, NewQueueOf(phase[2])),
                    new IntCodeComputer(memory, NewQueueOf(phase[3])),
                    new IntCodeComputer(memory, NewQueueOf(phase[4])),
                };

                var output = 0L;
                while (computers.Where(c => !c.Halted).Any()) {
                    foreach (var computer in computers)
                    {
                        if (computer.Output == -99)
                        {
                            continue;
                        }
                        computer.EnqueueInput(output);
                        computer.Compute();
                        output = computer.Output;
                    }
                }


                var currentOutput = computers.Last().Output;
                if (currentOutput > maxOutput)
                {
                    maxOutput = currentOutput;
                }
            }

            return maxOutput;
        }

        private static Queue<long> NewQueueOf(long phase)
        {
            var queue = new Queue<long>();
            queue.Enqueue(phase);
            return queue;
        }
    }
}
