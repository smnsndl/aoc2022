using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022
{
    class Program
    {
        static void Main(string[] args)
        {   
            string msg = " Advent of Code 2022 ";
            Console.WriteLine(string.Concat(Enumerable.Repeat("~", 80)));
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", 80)));
            Console.Write(string.Concat(Enumerable.Repeat("~", (80 - msg.Length)/2)));
            Console.Write(msg);
            Console.WriteLine(string.Concat(Enumerable.Repeat("~", (80 - msg.Length)/2)));
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", 80)));
            Console.WriteLine(string.Concat(Enumerable.Repeat("~", 80)));

            //Day1.Day1.Run();
            //Day2.Day2.Run();
            //Day3.Day3.Run();
            //Day4.Day4.Run();
            //Day5.Day5.Run();
            //Day6.Day6.Run();
            Day7.Day7.Run();

        }
    }
}
