using System.Collections;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace aoc2022.Day2
{
    public class Day2
    {
        
        public static void Run(string[] args)
        {
            Console.WriteLine("--- Day 2: Rock Paper Scissors ---");

            string[] data_file = File.ReadAllLines("./Day2/Data.txt");

            // Opponents hands
            // A B C Rock paper scissor
            // My hands
            // X Y Z Rock paper scissor

            #region part 1
            Console.WriteLine("Part 1");
            Dictionary<string, int> score = new() {
                { "A X", 1 + 3 }, { "A Y", 2 + 6 }, { "A Z", 3 + 0 },
                { "B X", 1 + 0 }, { "B Y", 2 + 3 }, { "B Z", 3 + 6 },
                { "C X", 1 + 6 }, { "C Y", 2 + 0 }, { "C Z", 3 + 3 }
            };

            int total_part1 = CalculateScore(score, data_file);
            Console.WriteLine($"If all goes according to plan: {total_part1}");
            #endregion

            #region part2
            Console.WriteLine("Part 2");

            Dictionary<string, int> score_part2 = new() {
                { "A X", 3 + 0 }, { "A Y", 1 + 3 }, { "A Z", 2 + 6 },
                { "B X", 1 + 0 }, { "B Y", 2 + 3 }, { "B Z", 3 + 6 },
                { "C X", 2 + 0 }, { "C Y", 3 + 3 }, { "C Z", 1 + 6 }
            };

            int total_part2 = CalculateScore(score_part2, data_file);

            Console.WriteLine($"If all goes according to plan: {total_part2}");
            #endregion
            Console.WriteLine(new string('-', 80));        

        }

        public static int CalculateScore(Dictionary<string, int> score_table, string[] data_input)
        {
            int total = 0;
            foreach (var line in data_input)
            {
                total += score_table[line];
            }
            return total;
        }
    }
}