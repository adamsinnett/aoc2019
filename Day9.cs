using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019
{
    class Day9
    {
        internal static void Execute(string problemInput)
        {
            Console.WriteLine("Day Nine");

            var memory = problemInput.Split(",").Select(long.Parse).ToList();
 
            var computer = new IntCodeComputer(memory.ToArray(), new Queue<long>(new List<long>() { 1L }));
            while (!computer.Halted)
            {                
                computer.Compute();
                computer.EnqueueInput(computer.Output);
            }


            Console.WriteLine("Part 1 = {0}", computer.Output);

            var computer2 = new IntCodeComputer(memory.ToArray(), new Queue<long>(new List<long>() { 2L }));
            while (!computer.Halted)
            {
                computer2.Compute();
                computer2.EnqueueInput(computer.Output);
            }
            Console.WriteLine("Part 2 = {0}", computer2.Output);
        }
    }
}
