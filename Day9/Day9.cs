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

            string[][] moves = File.ReadAllLines("..\\..\\..\\Day9\\Data.txt")
                                        .Select(r => r.Split(" ")).ToArray();         

            // Start (0,0);
            (int X, int Y) HeadPosition = (0, 0);
            (int X, int Y) TailPosition = (0, 0);
            // Log unique positions here for part 1 
            var visitedPositions = new HashSet<(int, int)>();

            for (var i=0; i < moves.Length; i++)
            {
                var moveDirection = moves[i][0];
                var moveDistance = int.Parse(moves[i][1].ToString());
                for(var d = 1; d <= moveDistance; d++)
                {
                    HeadPosition = Move(HeadPosition, moveDirection);
                    TailPosition = Follow(TailPosition, HeadPosition);
                    visitedPositions.Add(TailPosition);
                }
            }
            AnsiConsole.WriteLine($"Part 1: {visitedPositions.Count}");
        }
        private static (int, int) Move((int X, int Y) position, string direction)
        {
            switch (direction)
            {
                case "U":
                    position.Y++;
                    break;
                case "D":
                    position.Y--;
                    break;
                case "L":
                    position.X--;
                    break;
                case "R":
                    position.X++;
                    break;
            }
            return position;
        }
        public static (int,int) Follow((int X, int Y) position, (int X, int Y) target)
        {
            // Calculate delta 
            var stepX = target.X - position.X;
            var stepY = target.Y - position.Y;
            Console.WriteLine("DELTA " + stepX + " - " +stepY);
            if (Math.Abs(stepX) <= 1 && Math.Abs(stepY) <= 1)
            {
                // Touching Target position, don't move
                return position;
            }

            // Move max of one towards target
            position.X += Math.Sign(stepX);
            position.Y += Math.Sign(stepY);
            return position;
        }
    }
   
}