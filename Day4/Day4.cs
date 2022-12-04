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
            int total_with_any_overlap = 0;
            foreach(var row in data_file){
                List<int> assignment1 = row.Split(",")[0]
                                .Split("-")
                                .Select(number => Convert.ToInt32(number))
                                .ToList();
                List<int> assignment2 = row.Split(",")[1]
                                .Split("-")
                                .Select(number => Convert.ToInt32(number))
                                .ToList();
                                
                // Create range from the assignment values
                // First value in assigment is starting number
                // a1[1] - a1[0] + 1 calculates the number of elements to create, should end in the value from a1[1]
                var a1_range = Enumerable.Range(assignment1[0], assignment1[1] - assignment1[0] + 1);
                var a2_range = Enumerable.Range(assignment2[0], assignment2[1] - assignment2[0] + 1);
                
                // Check for diff in the ranges
                // If one range is fully contained in the other -> Except will be 0
                IEnumerable<int> differenceQuery = a1_range.Except(a2_range).ToList();
                IEnumerable<int> differenceQuery2 = a2_range.Except(a1_range).ToList();
                if(differenceQuery.Count() == 0 || differenceQuery2.Count() == 0){
                    Console.WriteLine("Found an assignment which is fully contained in the other");
                    total += 1;
                }

                // Part 2, any overlap
                bool overlap = a1_range.Intersect(a2_range).Any();
                if(overlap) {
                    total_with_any_overlap += 1;
                }
            }
            
            Console.WriteLine($"Part1: {total}");
            Console.WriteLine($"Part 2, any overlap count: {total_with_any_overlap}");
        }
    }
}
