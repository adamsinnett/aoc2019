using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AoC2019
{
    class Day3
    {
        public static void CrossedWires()
        {
            System.Console.WriteLine("Day Three");
            var input = getInput();
            var coords = input.ReadLines().Select(s => s.Split(",")).Select(s => FindCoordinates(s));
            var intersections = FindIntersections(coords.First(), coords.Last());

            var closestDistance = MinimumManhattanDistance(intersections, new Point(0, 0));
            System.Console.WriteLine("Part 1 answer is {0}", closestDistance);

            var fewestSteps = FindFewestCombinedSteps(coords.First(), coords.Last(), intersections);
            System.Console.WriteLine("Part 2 answer is {0}", fewestSteps);
        }

        private static object FindFewestCombinedSteps(List<Point> one, List<Point> two, List<Point> intersections)
        {
            var oneDistanceDict = new Dictionary<String, int>();
            var twoDistanceDict = new Dictionary<String, int>();
            for (int i = 0; i < one.Count; i++)
            {
                oneDistanceDict.TryAdd(one.ElementAt(i).GetKey(), i+1);
            }
            for (int i = 0; i < two.Count; i++)
            {
                twoDistanceDict.TryAdd(two.ElementAt(i).GetKey(), i+1);
            }

            int fewestSteps = int.MaxValue;

            foreach (var point in intersections)
            {
                var steps = oneDistanceDict.GetValueOrDefault(point.GetKey()) + twoDistanceDict.GetValueOrDefault(point.GetKey());
                if (steps < fewestSteps)
                {
                    fewestSteps = steps;
                }

            }

            return fewestSteps;
        }

        private static object MinimumManhattanDistance(List<Point> intersections, Point origin)
        {
            var minimum = int.MaxValue;
            foreach (var point in intersections)
            {
                var distance = Math.Abs(point.x - origin.x) + Math.Abs(point.y - origin.y);
                if (distance < minimum)
                {
                    minimum = distance;
                }
            }
            return minimum;
        }

        private static List<Point> FindIntersections(List<Point> wireOne, List<Point> wireTwo)
        {
            var wireTwoDict = new Dictionary<String, Point>();

            wireTwo.ForEach(p => wireTwoDict.TryAdd(p.GetKey(), p));

            var intersections = new List<Point>();
            foreach (var point in wireOne)
            {
                if (wireTwoDict.ContainsKey(point.GetKey()))
                {
                    intersections.Add(point);
                }
            }
            return intersections;
        }

        private static List<Point> FindCoordinates(string[] s)
        {
            var coordinates = new List<Point>();
            var current = new Point(0, 0);
            foreach (var inst in s)
            {
                int moveCount = int.Parse(inst.Substring(1, inst.Length - 1));
                Point next;
                switch (inst.Substring(0, 1))
                {
                    case "R":
                        for (int i = 0; i < moveCount; i++)
                        {
                            next = new Point(current.x + 1, current.y);
                            coordinates.Add(next);
                            current = next;
                        }
                        break;
                    case "L":
                        for (int i = 0; i < moveCount; i++)
                        {
                            next = new Point(current.x - 1, current.y);
                            coordinates.Add(next);
                            current = next;
                        }
                        break;
                    case "U":
                        for (int i = 0; i < moveCount; i++)
                        {
                            next = new Point(current.x, current.y + 1);
                            coordinates.Add(next);
                            current = next;
                        }
                        break;
                    case "D":
                        for (int i = 0; i < moveCount; i++)
                        {
                            next = new Point(current.x, current.y - 1);
                            coordinates.Add(next);
                            current = next;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

            }

            return coordinates;
        }

        // TODO make abstract
        private static string getInput()
        {
            var request = WebRequest.Create(@"https://adventofcode.com/2019/day/3/input");
            var cookie = new Cookie("session", "53616c7465645f5f2cce37a173e82fceb957550c8a2b9d5ef7de29ec23d14a58ebad77f8d483d319a263e45fd4bd36c9")
            {
                Domain = @"adventofcode.com"
            };

            request.TryAddCookie(cookie);
            var response = request.GetResponse();
            string input;
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                input = reader.ReadToEnd();
            }

            // Close the response.  
            response.Close();

            return input;
        }
    }

    class Point
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string GetKey()
        {
            return x.ToString() + "_" + y.ToString();
        }

        public override string ToString()
        {
            return "Point: " + x + " " + y;
        }
    }
}
