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

namespace aoc2022.Day9
{
    public class Day9
    {

        public static void Run()
        {
            var greeting = new Rule("[green]Day 9: Rope Bridge[/]");
            greeting.LeftAligned();
            greeting.Style = Style.Parse("green");
            AnsiConsole.Write(greeting);

            string[][] dataLines = File.ReadAllLines("..\\..\\..\\Day9\\Data.txt")
                                        .Select(r => r.Split(" "))
                                        .ToArray();

        }
    }
   
}