using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace aoc2022.day3
{
    public class Day3
    {
        
        public static void Run(string[] args)
        {
            string[] example = {"vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw"};
            string[] data_file = File.ReadAllLines("/home/simon/Dokument/codespace/csharp/aoc2022/day3/Data.txt");


            var part1 = data_file.Select(rucksack =>
                new ValueTuple<string, string>(
                    rucksack.Substring(0, rucksack.Length / 2),
                    rucksack.Substring(rucksack.Length / 2)))
            .Select(compartments => compartments.Item1.Intersect(compartments.Item2))
            .Aggregate(0, (sum, commonItem) => sum + GetPriority(commonItem.First()));
            Console.WriteLine(part1);
            
            var part2 = data_file.Chunk(3)
                .Select(group => group[2].Intersect(group[1].Intersect(group[0])))
                .Aggregate(0, (sum, commonItem) => sum + GetPriority(commonItem.First()));
            Console.WriteLine(part2);
        }
        public static int GetPriority(char item)
        {
            int value;
            if(item - 'a' >= 0){
                value = item - 'a' + 1;
            } else {
                value = item - 'A' + 27;
            }
            return value;
        }
    }
}
