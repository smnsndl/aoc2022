using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc2022.Day10
{
    public class Day10
    {

        public static void Run()
        {
            var greeting = new Rule("[green]Day 10: Cathode-Ray Tube[/]");
            greeting.LeftAligned();
            greeting.Style = Style.Parse("green");
            AnsiConsole.Write(greeting);

            //string[] operations = File.ReadAllLines("./Day10/Example.txt");
            string[] operations = File.ReadAllLines("./Day10/Data.txt");
            var output = cycle(operations);

            var part1 = output.Select((register, i) => (i + 1) * register)
                .Where((register, i) => i == 19 || i == 59 || i == 99 || i == 139 || i == 179 || i == 219)
                .Sum();

            AnsiConsole.WriteLine(part1);


            AnsiConsole.MarkupLine(String.Join(Environment.NewLine, cycle(operations)
                .Select((register, i) =>
                {
                    Console.WriteLine($"{register} {i}");
                    var pixel = i % 40;
                    if (pixel == register - 1 || pixel == register || pixel == register + 1)
                    {
                        return '#';
                    }
                    else
                    {
                        return '.';
                    }
                })
                .Chunk(40)
                .Select(row => new string(row))
            ));

        }
        private static IEnumerable<int> cycle(string[] input)
        {
            var X = 1;

            foreach (var ins in input)
            {
                if (ins.StartsWith("noop"))
                {
                    yield return X;
                }
                else
                {
                    yield return X;
                    yield return X;

                    X += int.Parse(ins.Split(" ").Last());
                }
            }
        } 
    }


   
}