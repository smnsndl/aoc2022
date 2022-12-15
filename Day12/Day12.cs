using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day12
{
    public class Day12
    {
        public static void Run()
        {
            var greeting = new Spectre.Console.Rule("[green]Day 12: Hill Climbing Algorithm[/]\n");
            greeting.LeftAligned();
            greeting.Style = Style.Parse("green");
            AnsiConsole.Write(greeting);

            var timer = new Stopwatch();
            timer.Start();

            string[] example = File.ReadAllLines("..\\..\\..\\Day12\\example.txt");
            //string[] data = File.ReadAllLines("..\\..\\..\\Day11\\data.txt");
            int[,] map = Parse(example);
        }

        public static int[,] Parse(string[] lines)
        {
            int[,] heights = new int[lines.Length, lines[0].Length];

           
            return heights;
        }
    }

}
