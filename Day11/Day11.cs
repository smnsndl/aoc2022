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
            string[] data = File.ReadAllLines("..\\..\\..\\Day11\\data.txt");

            var monkeys = ParseInput(data);
            int ROUNDS_P1 = 20;
            DoRounds(monkeys, ROUNDS_P1, 3);
            var topTwoBusiestMonkeys = new List<Monkey>(monkeys.OrderByDescending(m => m.numberOfInspections).Take(2));
            var monkeyBusiness = topTwoBusiestMonkeys[0].numberOfInspections * topTwoBusiestMonkeys[1].numberOfInspections;
            Console.WriteLine($"\nPart 1: {monkeyBusiness}");


            monkeys = ParseInput(data);
            int ROUNDS_P2 = 10_000;
            // Creating a 'supermodulo' from all the values in lines "Test divisible by X"
            long supermodulo = 1;
            foreach (var monkey in monkeys)
                supermodulo *= monkey.mod;
            DoRounds(monkeys, ROUNDS_P2, supermodulo);
            topTwoBusiestMonkeys = new List<Monkey>(monkeys.OrderByDescending(m => m.numberOfInspections).Take(2));
            monkeyBusiness = topTwoBusiestMonkeys[0].numberOfInspections * topTwoBusiestMonkeys[1].numberOfInspections;
            Console.WriteLine($"\nPart 2: {monkeyBusiness}");

        }

        private static List<Monkey> ParseInput(string[] data)
        {

            List<Monkey> monkeys = new List<Monkey>();
            var currentMonkey = new Monkey();
            foreach (var x in data)
            {
                var line = x.Trim().Split(':', ' ', ',');
                if (line.Length == 1)
                {
                    continue;
                }
                switch (line[0], line[1])
                {
                    case ("Monkey", _):
                        currentMonkey = new Monkey();
                        break;
                    case ("Starting", _):
                        currentMonkey.startingItems = new Queue<long>(line[2..]
                                            .Select(x => long.TryParse(x, out long xInt) ? xInt : -1)
                                            .Where(x => x > -1));
                        break;
                    case ("Operation", _):
                        currentMonkey.operation = line[^2] switch
                        {
                            "+" => x => x + (int.TryParse(line[^1], out int a) ? a : x),
                            "-" => x => x - (int.TryParse(line.Last(), out int a) ? a : x),
                            "*" => x => x * (int.TryParse(line.Last(), out int a) ? a : x),
                            "/" => x => x / (int.TryParse(line.Last(), out int a) ? a : x),
                            _ => x => x
                        };
                        break;
                    case ("Test", _):
                        currentMonkey.test = x => x % (int.TryParse(line.Last(), out int a) ? a : x) == 0;
                        currentMonkey.mod = long.Parse(line.Last()); // for part2
                        break;
                    case ("If", "true"):
                        if (int.TryParse(line.Last(), out int t))
                            currentMonkey.TrueMonkey = t;
                        else
                            currentMonkey.TrueMonkey = -1;
                        break;
                    case ("If", "false"):
                        if (int.TryParse(line.Last(), out int f))
                            currentMonkey.FalseMonkey = f;
                        else
                            currentMonkey.FalseMonkey = -1;
                        monkeys.Add(currentMonkey);
                        break;
                    default: break;
                }
            }
            return monkeys;
        }

        private static List<Monkey> DoRounds(List<Monkey> monkeys, int ROUNDS, long worryLevelDecreaser)
        {
            foreach (var round in Enumerable.Range(0, ROUNDS))
            {
                foreach (var monkey in monkeys)
                {
                    // While monkey still has item trydequeue
                    while (monkey.startingItems.TryDequeue(out long worryLevel))
                    {
                        worryLevel = monkey.operation!(worryLevel);

                        if (ROUNDS == 20) // Part 1
                            worryLevel = (long)Math.Floor(worryLevel / (Decimal)worryLevelDecreaser);
                        if (ROUNDS == 10_000) // Part 2 
                            worryLevel %= worryLevelDecreaser;

                        if (monkey.test!(worryLevel))
                        {
                            monkeys[monkey.TrueMonkey].startingItems.Enqueue(worryLevel);
                        }
                        else
                        {
                            monkeys[monkey.FalseMonkey].startingItems.Enqueue(worryLevel);
                        }
                        monkey.numberOfInspections++;
                    }
                }
                // For printing like the Example
                /*if (round == 0 || round == 19 || round == 999 || round == 1999 ||
                    round == 2999 || round == 3999 || round == 4999 || round == 5999 ||
                    round == 6999 || round == 7999 || round == 8999 || round == 9999)
                {
                    Console.WriteLine($"Round {round + 1}");
                    foreach (var monkey in monkeys)
                    {
                        Console.WriteLine($"Monkey {monkeys.IndexOf(monkey)} inspected items {monkey.numberOfInspections}");
                    }
                }*/
            }

            return monkeys;
        }


        private class Monkey
        {
            public Queue<long> startingItems = new Queue<long>();
            public Func<long, long>? operation;
            public Predicate<long>? test;
            public int TrueMonkey;
            public int FalseMonkey;
            public long numberOfInspections = 0;
            public long mod = 0;
        }

    }

}
