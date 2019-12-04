using System.IO;
using System.Linq;
using System.Net;

namespace AoC2019
{
    class DayOne
    {
        public const string path = @"C:\Users\quand\projects\day1.txt";
        public static void getFuel(string input)
        {
            System.Console.WriteLine("Day One");

            var mass = input.ReadLines()
                .Select(int.Parse)
                .ToArray();

            var part1 = mass
                .Select(m => FuelCalculator(m, false))
                .Aggregate(0, (memo, fuel) => memo + fuel);

            var part2 = mass
                .Select(m => FuelCalculator(m, true))
                .Aggregate(0, (memo, fuel) => memo + fuel);

            System.Console.WriteLine("Part 1 is {0}", part1);
            System.Console.WriteLine("Part 2 is {0}", part2);
            
        }

        static int FuelCalculator(int mass, bool accountForFuel)
        {
            int fuel = (mass / 3) - 2;
            if (!accountForFuel) return fuel;

            return (fuel <= 0) ? 0 : fuel + FuelCalculator(fuel, accountForFuel);
        }
    }
}
