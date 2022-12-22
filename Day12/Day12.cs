using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day12
{
    public class Day12
    {

        public static void Run()
        {
            var greeting = new Spectre.Console.Rule("[green]Day 12: Hill Climbing Algorithm[/]\n");
            greeting.LeftAligned();
            greeting.Style = Style.Parse("green");
            AnsiConsole.Write(greeting);

            var timer = new Stopwatch();
            timer.Start();

            //var example = File.ReadAllLines("..\\..\\..\\Day12\\example.txt");
            var data = File.ReadAllLines("..\\..\\..\\Day12\\data.txt");
            var width = data[0].Length;
            var height = data.Length;

        #region part1
            var (map, startPosition, endPosition) = ParseInput(data);
            AnsiConsole.WriteLine("Part 1");
            AnsiConsole.WriteLine($"Start position {startPosition}\nEnd {endPosition}");

            // Set start/end actual values
            map[startPosition.First().X, startPosition[0].Y] = 'z';
            map[endPosition.X, endPosition.Y] = 'z';
            Console.WriteLine($"Part1: {FindPath(map, startPosition[0], endPosition)}");
        #endregion

        #region part2
            AnsiConsole.WriteLine("\nPart 2");
            var (map_p2, startPositions_p2, endPosition_p2) = ParseInput(data, 'a', 'E');
            map_p2[endPosition_p2.X, endPosition_p2.Y] = 'z';

            AnsiConsole.WriteLine($"Found {startPositions_p2.Count} starting positions");
            startPositions_p2.ForEach(startpos => map_p2[startpos.X, startpos.Y] = 'a');
            List<int> part2Steps = new List<int>();
            startPositions_p2.ForEach(startpos => part2Steps.Add(FindPath(map_p2, startpos, endPosition_p2)));

            AnsiConsole.WriteLine($"Part 2: {part2Steps.Min()}");

        #endregion
            timer.Stop();
            AnsiConsole.WriteLine(timer.Elapsed.ToString());
        }

        private static (char[,] map, List<Point> startPosition, Point endPosition) ParseInput(string[] data, char start = 'S', char end = 'E')
        {
            var width = data[0].Length;
            var height = data.Length;
            var map = new char[width, height];

            List<Point> startPositions = new() { };
            Point endPosition = default;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = data[y][x];
                    if (map[x, y] == 'S' || map[x, y] == start)
                    {
                        var newPoint = new Point(x, y);
                        if (!startPositions.Contains(newPoint))
                        {
                            startPositions.Add(newPoint);
                        }
                    }
                    else if (map[x, y] == end)
                    {
                        endPosition = new Point(x, y);
                    }
                }
            }

            return (map, startPositions, endPosition);
        }


        private static int FindPath(char[,] map, Point startPosition, Point endPosition)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            var steps = new int[width, height];

            var queue = new Queue<Point>();
            queue.Enqueue(startPosition);

            while (queue.Count > 0)
            {
                var currentPoint = queue.Dequeue();
                var nextSteps = steps[currentPoint.X, currentPoint.Y] + 1;
                foreach (var direction in Directions.WithoutDiagonals)
                {
                    var newPosition = new Point(currentPoint.X + direction.X,
                                                currentPoint.Y + direction.Y);
                    if (newPosition.X >= 0 && newPosition.Y >= 0 &&
                        newPosition.X < width && newPosition.Y < height &&
                        steps[newPosition.X, newPosition.Y] == 0 &&
                        newPosition != startPosition)
                    {
                        // Compare char values
                        if (map[newPosition.X, newPosition.Y] <= map[currentPoint.X, currentPoint.Y] + 1)
                        {
                            if (newPosition == endPosition)
                            {
                                return nextSteps;
                            }

                            steps[newPosition.X, newPosition.Y] = nextSteps;
                            queue.Enqueue(newPosition);
                        }
                    }
                }
            }
            return int.MaxValue;
        }


        private static class Directions
        {
            public static Point[] WithoutDiagonals { get; } = new Point[]
            {
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, -1),
                new Point(-1, 0),
            };
        }
    }
}
