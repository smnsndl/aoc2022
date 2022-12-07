using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc2022.Day5
{
    public class Day5
    {
        public static Regex MoveFromTo = new Regex("move (\\d+) from (\\d+) to (\\d+)", RegexOptions.IgnoreCase);

        public static void Run()
        {
            Console.WriteLine("--- Day 5: Supply Stacks ---\r\n");
            List<List<Stack>> myList = new();
            //string[] example_lines = File.ReadAllLines("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day5\\Example.txt");
            string[] data_file = File.ReadAllLines("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day5\\Data.txt");

         
            Console.WriteLine(SolvePart1(data_file));
            Console.WriteLine(SolvePart2(data_file));


            List<string> headers = new();
            foreach(var row in data_file)
            {
                if (String.IsNullOrEmpty(row))
                {
                    break;
                }
                headers.Add(row);
            }
            var header_transpose = headers.SelectMany(inner => inner.Select((item, index) => new { item, index }))
                                        .GroupBy(i => i.index, i => i.item)
                                        .Select(g => g.ToList())
                                        .ToList();

            var crates = data_file.Where(l => l.Length != 0 && l[0] != 'm');

            //Console.WriteLine(string.Join("\n", crates1.ToArray()));



           // Console.WriteLine(string.Join("\n", headers.ToArray()));


        }

        static (Stack<char>[], int) ParseInput(string[] lines)
        {
            int initialCrates = lines.Where(l => l.Length != 0 && l[0] != 'm').Count() - 1;
            int crateCount = lines[initialCrates].Where(c => c != '[' && c != ' ' && c != ']').Count();

            Stack<char>[] crates = new Stack<char>[crateCount].Select(s => new Stack<char>()).ToArray();

            for (int i = initialCrates - 1; i >= 0; i--)
            {
                string line = lines[i];
                
                for (int j = 0, k = 1; j < crateCount; j++, k += 4)
                {
                    if (line[k] == ' ') continue;
                    crates[j].Push(line[k]);
                }
            }

            return (crates, initialCrates + 2);
        }

        public static string SolvePart1(string[] lines)
        {

            (Stack<char>[] crates, int instructionLine) = ParseInput(lines);
            for (int i = instructionLine; i < lines.Length; i++)
            {
                int[] nums = MoveFromTo.Matches(lines[i])[0].Groups.Cast<Group>()
                    .Skip(1)
                    .Select(g => int.Parse(g.Value))
                    .ToArray();

                Enumerable.Range(0, nums[0])
                    .Select(n => crates[nums[1] - 1]
                    .Pop())
                    .ToList()
                    .ForEach(c => crates[nums[2] - 1].Push(c));
            }
            return new string(crates.Select(s => s.Peek()).ToArray());
        }
        public static string SolvePart2(string[] lines)
        {
            (Stack<char>[] crates, int instructionLine) = ParseInput(lines);
            for (int i = instructionLine; i < lines.Length; i++)
            {
                int[] nums = MoveFromTo.Matches(lines[i])[0].Groups.Cast<Group>()
                    .Skip(1)
                    .Select(g => int.Parse(g.Value))
                    .ToArray();

                Enumerable.Range(0, nums[0])
                    .Select(n => crates[nums[1] - 1]
                    .Pop())
                    .Reverse()
                    .ToList()
                    .ForEach(c => crates[nums[2] - 1].Push(c));
            }
            return new string(crates.Select(s => s.Peek()).ToArray());
        }
    }
   
}
