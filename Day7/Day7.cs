using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc2022.Day7
{
    public class Day7
    {

        public static void Run()
        {
            Console.WriteLine("--- Day 7: No Space Left On Device ---\r\n");
            string[] dataLines = File.ReadAllLines("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day7\\Example.txt");
            var workingDirectory = "";
            var paths = new Dictionary<string, int>();
            for (int i = 0; i < dataLines.Length; i += 1)
            {
                var currentLine = dataLines[i];
                if (!currentLine.StartsWith('$')) continue; // if not a commandline keep looping
                var parts = currentLine.Split(' ');
                var command = parts.ElementAt(1);
                // parts.Length when cd is "$", "cd", "<dir>" = 3 parts
                // when ls is "$", "ls" = only 2 parts
                var args = parts.Length > 2 ? parts.Last() : "";
                Console.WriteLine($"Command:{command}, Args:{args}");

                switch (command)
                {
                    case "cd":
                        // Going into new directory, break loop, search for next command
                        workingDirectory = Path.GetFullPath(Path.Combine(workingDirectory, args));
                        Console.WriteLine($">Changing working directory to {workingDirectory}");
                        break;
                    case "ls":
                        {
                            // Keep taking output lines until next command $ is reached
                            var output = dataLines.Skip(i + 1).TakeWhile(line => !line.StartsWith('$'));
                            Console.WriteLine($"\t{string.Join("\n", output)}");

                            // Calculate sum file sizes from the output, dirs ín dataLines output don't have sizes
                            var sizes = output.Select(line => line.Split(' ').First())
                                .Where(line => line.All(char.IsNumber))
                                .Sum(int.Parse);
                            Console.WriteLine($"\tSummarized file sizes {sizes}");
                            paths.TryAdd(workingDirectory, sizes);
                            break;
                        }
                }
            }

            Console.WriteLine("\nDirectory sizes based on subfile sizes alone");
            foreach (var p in paths)
            {
                Console.WriteLine($"\t{p}");
            }

            Console.WriteLine("\nSummarizing subdirs sizes onto parentdir size");
            // Calculate the directory sizes
            // Example key=C:\A, value=0
            // key C:\A\A, C:\A\B, C:\A\C all share same key from p1
            // Sum those sizes onto p1 in paths to get full directory size for C:\A
            foreach (var p1 in paths)
            {
                foreach (var p2 in paths)
                {
                    if(p1.Key != p2.Key && p2.Key.StartsWith(p1.Key)) {
                        paths[p1.Key] += p2.Value;
                    }
                }
            }

            foreach (var p in paths)
            {
                Console.WriteLine($"\t{p}");
            }

            // part 1 result
            // Find values less than equal to 100_000, summerize
            var partOneResult = paths.Where(f => f.Value <= 100_000).Sum(f => f.Value);
            Console.WriteLine($"Part 1 - {partOneResult}\n");

            // Part 2
            const int total_disk_space = 70_000_000;
            const int required_space = 30_000_000;
            int currentOccupiedSpace = paths["C:\\"];
            int currentFreeSpace = total_disk_space - currentOccupiedSpace;
            int currentRequiredFreeSpace = required_space - (total_disk_space - currentOccupiedSpace);
            Console.WriteLine($"currentOccupiedSpace {currentOccupiedSpace} currentFreeSpace {currentFreeSpace}");
            Console.WriteLine($"currentRequiredFreeSpace {currentRequiredFreeSpace}");

            var paths_bigger_than_required_space = paths.Where(f => f.Value >= currentRequiredFreeSpace);
            var partTwoResult = paths_bigger_than_required_space.Min(f => f.Value);
            Console.WriteLine($"Part 2 - {partTwoResult}");
        }

    }
   
}
