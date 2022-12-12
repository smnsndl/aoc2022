using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022.Day11
{
    public class Day11
    {
        public static void Run()
        {
            var greeting = new Spectre.Console.Rule("[green]Day 11: Monkey in the Middle[/]");
            greeting.LeftAligned();
            greeting.Style = Style.Parse("green");
            AnsiConsole.Write(greeting);

            string[] example = File.ReadAllLines("..\\..\\..\\Day11\\example.txt");
            List<Monkey> monkeys = new List<Monkey>();
            var currentMonkey = new Monkey();
            foreach (var x in example)
            {
                var line = x.Trim().Split(':', ' ', ',');
                Console.WriteLine(string.Join(", ", line));
                if (line.Length == 1) {
                    continue;
                }
                switch (line[0], line[1])
                {
                    case ("Monkey", _):
                        Console.WriteLine(">>>New monkey");
                        currentMonkey = new Monkey();
                        break;
                    case ("Starting", _): 
                        currentMonkey.startingItems = new Queue<int>(line[2..]
                                            .Select(x => int.TryParse(x, out int xInt) ? xInt : -1)
                                            .Where(x => x > -1));
                        Console.WriteLine($">>>{string.Join(", ", currentMonkey.startingItems)}");
                        break;
                    default:break;
                }

            }

        }
        private class Monkey 
        {
            public Queue<int> startingItems = new Queue<int>();
            public Func<long, long>? operation;
            public Predicate<long>? test;
            public int TrueMonkey;
            public int FalseMonkey;
            public long numberOfInspections = 0;
        }

    }

}
