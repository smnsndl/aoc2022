using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc2022.Day6
{
    public class Day6
    {

        public static void Run()
        {
            Console.WriteLine("--- Day 6: Tuning Trouble ---\r\n");
            char[] example1 = "mjqjpqmgbljsphdztnvjfqwrcgsmlb".ToCharArray();
            char[] example2 = "bvwbjplbgvbhsrlpgdmjqwftvncz".ToCharArray();
            char[] example3 = "nppdvjthqldpwncqszvftbrmjlhg".ToCharArray();
            char[] example4 = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg".ToCharArray();
            char[] example5 = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw".ToCharArray();

            Console.WriteLine("Examples");
            Console.WriteLine($"{new string(example1)} - {Tuner(example1, 4)}");
            Console.WriteLine($"{new string(example2)} - {Tuner(example2, 4)}");
            Console.WriteLine($"{new string(example3)} - {Tuner(example3, 4)}");
            Console.WriteLine($"{new string(example4)} - {Tuner(example4, 4)}");
            Console.WriteLine($"{new string(example5)} - {Tuner(example5, 4)}");


            Console.WriteLine("Input file");
            char[] data_file = File.ReadAllText("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day6\\Data.txt").ToCharArray();
            Console.WriteLine($"Part 1: {Tuner(data_file, 4)}");
            Console.WriteLine($"Part 2: {Tuner(data_file, 14)}");
        }

        public static int Tuner(char[] input, int search_length) {
            for (int i = 0; i < input.Length; i++)
            {
                var h = input.Skip(i).Take(search_length);
                var d = new HashSet<char>(h);
                if (d.Count == search_length)
                {
                    return i + search_length;
                }
            }
            return -1; // marker not found
        }
    }
   
}
