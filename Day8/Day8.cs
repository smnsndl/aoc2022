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

namespace aoc2022.Day8
{
    public class Day8
    {

        public static void Run()
        {
            Console.WriteLine("--- Day 8: Treetop Tree House ---\r\n");
            string[] dataLines = File.ReadAllLines("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day8\\Example.txt");
            int[,] tree_array = ParseInputTo2DArray(dataLines);

            Console.WriteLine("Printing out tree map");
            for (int x = 0; x < tree_array.GetLength(0); x += 1)
            {
                for (int y = 0; y < tree_array.GetLength(1); y += 1)
                {
                    Console.Write(tree_array[x,y]);
                }
                Console.Write("\n");
            }

        }

        private static int[,] ParseInputTo2DArray(string[] input)
        {
            int[,] tree_array = new int[input.Length, input[0].Length];
            for (int x = 0; x < input.Length; x += 1)
            {
                for (int y = 0; y < input[x].Length; y += 1)
                {
                    int tree = int.Parse(input[x][y].ToString());
                    tree_array[x, y] = tree;
                }
            }
            return tree_array;
        }


    }
   
}