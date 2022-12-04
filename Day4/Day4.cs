using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace aoc2022.Day4
{
    public class Day4
    {
        
        public static void Run(string[] args)
        {
            Console.WriteLine("Day 4");
            //string[] data_file = File.ReadAllLines("/home/simon/Dokument/codespace/csharp/aoc2022/Day4/Example.txt");
            string[] data_file = File.ReadAllLines("/home/simon/Dokument/codespace/csharp/aoc2022/Day4/Data.txt");

            int total = 0;
            int any_overlap = 0;
            foreach(var x in data_file){
                var assignment1 = x.Split(",")[0];
                var assignment2 = x.Split(",")[1];
                List<int> a1 = assignment1
                                .Split("-")
                                .Select(number => Convert.ToInt32(number))
                                .ToList();
                List<int> a2 = assignment2
                                .Split("-")
                                .Select(number => Convert.ToInt32(number))
                                .ToList();
                var a1_range = Enumerable.Range(a1[0], a1[1] - a1[0] + 1);
                var a2_range = Enumerable.Range(a2[0], a2[1] - a2[0] + 1);
                
                IEnumerable<int> differenceQuery = a1_range.Except(a2_range).ToList();
                IEnumerable<int> differenceQuery2 = a2_range.Except(a1_range).ToList();

                if(differenceQuery.Count() == 0 || differenceQuery2.Count() == 0){
                    Console.WriteLine("Found an assignment which is fully contained in the other");
                    total += 1;
                }
                Console.WriteLine($"Part1: {total}");

                // Part 2, any overlap
                bool overlap = a1_range.Intersect(a2_range).Any();
                if(overlap) {
                    any_overlap += 1;
                }
                Console.WriteLine($"Part 2, any overlap count: {any_overlap}");
            }
        }
    }
}
