using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc2022.Day5
{
    public class Day5
    {
        public static void Run(string[] args)
        {
            Console.WriteLine("--- Day 5: Supply Stacks ---\r\n");
            List<List<Stack>> myList = new();
            string[] example_lines = File.ReadAllLines("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day5\\Example.txt");
            //string[] data_file = File.ReadAllLines(".\\Day5\\Data.txt");

            (Stack<char>[] crates, int instructionLineBeginning) = ParseInput(example_lines);
            foreach (var crate in crates)
            {
                foreach(var c in crate)
                    Console.WriteLine(c);

            }

            Console.WriteLine(instructionLineBeginning);
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

        
    }
}
