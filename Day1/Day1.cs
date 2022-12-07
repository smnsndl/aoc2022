using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace aoc2022.Day1
{
    public class Day1
    {
        public static void Run()
        {
            Console.WriteLine("--- Day 1: Calorie Counting ---");

            /*
             // example list.. set values as string
             // Going to read real data input from a file, values will be string
             var calorie_list = new ArrayList(){
                "1000",
                "2000",
                "3000",
                "",
                "4000",
                "",
                "5000",
                "6000",
                "",
                "7000",
                "8000",
                "9000",
                "",
                "10000" };*/
            #region part1
            string[] data_file = File.ReadAllLines("./Day1/Data.txt");
            var calorie_list = new List<string>(data_file);

            var sum_list = new List<int>();
            int current_elf_calorie_sum = 0;
            foreach(string calorie in calorie_list){
                // Console.WriteLine(calorie);
                if (string.IsNullOrEmpty(calorie))
                {
                    // Console.WriteLine($"New Elf, current sum {current_elf_calorie_sum}");
                    sum_list.Add(current_elf_calorie_sum);
                    current_elf_calorie_sum = 0;
                }
                else
                {
                    current_elf_calorie_sum += int.Parse(calorie);

                }
            }

            Console.WriteLine("Part 1");
            Console.WriteLine($"Number of elves: {sum_list.Count}");
            sum_list.Sort(); // sorting ascending order
            Console.WriteLine($"Most carried calories by one elf: {sum_list[sum_list.Count - 1]}");
            #endregion

            #region part 2
            Console.WriteLine("\nPart 2");
            var top_three = sum_list.TakeLast(3);
            var top_three_sum = top_three.Sum();
            Console.WriteLine($"Sum top three calories: {top_three_sum}");
            #endregion

            Console.WriteLine(new string('-', 80));
        }
    }
}