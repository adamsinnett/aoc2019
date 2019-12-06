using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019
{
    class Day6
    {
        public static void Execute(string input)
        {
            System.Console.WriteLine("Day Six");
            var orbits = GraphOrbits(input.ReadLines());
            //var orbits = GraphOrbits("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN".ReadLines());
            int indirectPaths = FindIndirectPaths(orbits);

            var part1 = indirectPaths;
            Console.WriteLine("Part 1 is {0}", part1);
            var part2 = FindMoves(orbits); 
            Console.WriteLine("Part 2 is {0}", part2);

        }

        private static int FindMoves(Dictionary<string, List<(string, string)>> orbits)
        {

            var target = FindOrbitFromDest("YOU", orbits);
            var upstream = target.Item1;
            var countToSan = FindCountToSanta(upstream, orbits);
            var upstreamMoves = 0;
            while (countToSan == -1)
            {
                target = FindOrbitFromDest(target.Item1, orbits);
                upstream = target.Item1;
                countToSan = FindCountToSanta(upstream, orbits);
                upstreamMoves++;
            }
            Console.WriteLine(upstreamMoves);
            Console.WriteLine(countToSan);

            return countToSan + upstreamMoves;
        }

        private static int FindCountToSanta(string key, Dictionary<string, List<(string, string)>> orbits)
        {
            var paths = orbits.GetValueOrDefault(key, new List<(string, string)>());
          
            if (key == "SAN")
            {
                return 0;
            }

            foreach (var path in paths)
            {
                if (path.Item2 == "SAN")
                {
                    return 0;
                }

                var count = FindCountToSanta(path.Item2, orbits);
                if ( count >= 0)
                {
                    Console.WriteLine(path.Item2);
                    Console.WriteLine(count);
                    return ++count;
                }

            }

            return -1;
        }

        private static (string, string) FindOrbitFromDest(string dest, Dictionary<string, List<(string, string)>> orbits)
        {
            return orbits.Values.SelectMany(s => s).Where(o => o.Item2 == dest).First();
        }

        private static int FindIndirectPaths(Dictionary<string, List<(string, string)>> orbits)
        {
            var links = new HashSet<(string, string)>();

            foreach (var key in orbits.Keys)
            {
                FindEntirePath(key, orbits).ForEach(p => links.Add(p));

            }

            return links.Count();
        }

        private static List<(string, string)> FindEntirePath(string key, Dictionary<string, List<(string, string)>> orbits)
        {
            var paths = orbits.GetValueOrDefault(key, new List<(string, string)>());
            if (paths == null || paths.Count() == 0)
            {
                return new List<(string, string)>();
            }

            var indirect = paths.Select(p => FindEntirePath(p.Item2, orbits)).SelectMany(p => p.Select(s => (key, s.Item2)).ToList()).ToList();
            indirect.AddRange(paths);
            return indirect;
        }

        private static Dictionary<string, List<(string, string)>> GraphOrbits(IEnumerable<string> orbits)
        {
            return orbits.Select(o => o.Split(")")).Aggregate(new Dictionary<string, List<(string, string)>>(), (dict, orbit) => {
                var direct = dict.GetValueOrDefault(orbit[0], new List<(string, string)>());
                direct.Add((orbit[0], orbit[1]));
                dict.TryAdd(orbit[0], direct);
                return dict;
            });
        }
    }
}
