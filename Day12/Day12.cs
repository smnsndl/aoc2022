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

            //var data = File.ReadAllLines("..\\..\\..\\Day12\\example.txt");
            var data = File.ReadAllLines("..\\..\\..\\Day12\\data.txt");
            var width = data[0].Length;
            var height = data.Length;
            #region part1
            var (map, steps, startPosition, endPosition) = ParseInput(data, 'S', 'E');
            AnsiConsole.WriteLine("Part 1");
            AnsiConsole.WriteLine($"Start position {startPosition}\nEnd {endPosition}");

            // start/end values
            map[startPosition.X, startPosition.Y] = 'z';
            map[endPosition.X, endPosition.Y] = 'z';
            var queue = new Queue<Point>();
            queue.Enqueue(startPosition);
            //Console.WriteLine($"Testing {Solve2(map,startPosition,endPosition)}");
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var nextSteps = steps[current.X, current.Y] + 1;
                foreach (var direction in Directions.WithoutDiagonals)
                {
                    var newPosition = new Point(current.X + direction.X, current.Y + direction.Y);
                    if (newPosition.X >= 0 &&
                        newPosition.Y >= 0 &&
                        newPosition.X < width &&
                        newPosition.Y < height &&
                        steps[newPosition.X, newPosition.Y] == 0 &&
                        newPosition != startPosition)
                    {
                        if (map[newPosition.X, newPosition.Y] <= map[current.X, current.Y] + 1)
                        {
                            if (newPosition == endPosition)
                            {
                                AnsiConsole.WriteLine($"Answer:{nextSteps}");
                            }

                            steps[newPosition.X, newPosition.Y] = nextSteps;
                            queue.Enqueue(newPosition);
                        }
                    }
                }
            }
            #endregion

            #region part2
            AnsiConsole.WriteLine("\nPart 2");
            var (map2, steps2, startPositions2, endPosition2) = ParseInputP2(data, 'a', 'E');
            map2[endPosition2.X, endPosition2.Y] = 'z';

            AnsiConsole.WriteLine($"Found {startPositions2.Count} starting positions:");
            foreach (var p in startPositions2)
            {
                AnsiConsole.WriteLine($"{String.Join(",", p)}");

            }

            List<int> part2Steps = new List<int>();
            foreach (var startpos in startPositions2)
            {
                int currsteps = Solve2(map2, startpos, endPosition2);
                part2Steps.Add(currsteps);
                AnsiConsole.WriteLine(currsteps);
            }

            AnsiConsole.WriteLine("part2?");
            AnsiConsole.WriteLine(part2Steps.Min());

            #endregion
            timer.Stop();
            AnsiConsole.WriteLine(timer.Elapsed.ToString());
        }

        private static (char[,] map, int[,] steps, Point startPosition, Point endPosition) ParseInput(string[] data, char start, char end)
        {
            var width = data[0].Length;
            var height = data.Length;
            var map = new char[width, height];
            var steps = new int[width, height];
            Point startPosition = default;
            Point endPosition = default;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = data[y][x];
                    if (map[x, y] == start)
                    {
                        startPosition = new Point(x, y);
                    }
                    else if (map[x, y] == end)
                    {
                        endPosition = new Point(x, y);
                    }
                }
            }

            return (map, steps, startPosition, endPosition);
        }

        private static (char[,] map, int[,] steps, List<Point> startPosition, Point endPosition) ParseInputP2(string[] data, char start, char end)
        {
            var width = data[0].Length;
            var height = data.Length;
            var map = new char[width, height];
            var steps = new int[width, height];
            List<Point> startPositions = new List<Point> {};
            Point endPosition = default;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = data[y][x];
                    if (map[x,y] == 'S' || map[x,y]==start)
                    {
                        map[x, y] = 'a';
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

            return (map, steps, startPositions, endPosition);
        }


        private static int Solve2(char[,] map, Point startPosition, Point endPosition)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            var steps = new int[width, height];

            var queue = new Queue<Point>();
            queue.Enqueue(startPosition);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var nextSteps = steps[current.X, current.Y] + 1;
                foreach (var direction in Directions.WithoutDiagonals)
                {
                    var newPosition = new Point(current.X + direction.X, current.Y + direction.Y);
                    if (newPosition.X >= 0 &&
                        newPosition.Y >= 0 &&
                        newPosition.X < width &&
                        newPosition.Y < height &&
                        steps[newPosition.X, newPosition.Y] == 0 &&
                        newPosition != startPosition)
                    {
                        if (map[newPosition.X, newPosition.Y] <= map[current.X, current.Y] + 1)
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
