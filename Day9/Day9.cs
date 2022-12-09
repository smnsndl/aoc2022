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
            Console.WriteLine("--- Day 8: Treetop Tree House ---\r\n");
            string[] dataLines = File.ReadAllLines("C:\\Users\\simsun\\source\\repos\\aoc2022\\Day8\\Data.txt");
            int[,] treeArray = ParseInputTo2DArray(dataLines);

            int treeRows = treeArray.GetLength(0); // starts at 1 
            int treeCols = treeArray.GetLength(1);

            bool[,] visibleTreesArray = new bool[treeRows, treeCols];
            int visibleTreesSum = 0;

            for (int currentRow = 0; currentRow < treeRows; currentRow++)
                for (int currentCol = 0; currentCol < treeCols; currentCol++)
                {
                    var height = treeArray[currentRow, currentCol];
                    // WEST
                    var visible = true;
                    for (int j = 0; visible && j < currentCol; j++)
                        if (treeArray[currentRow, j] >= height)
                            visible = false;

                    if (visible)
                    {
                        visibleTreesSum++;
                        visibleTreesArray[currentRow, currentCol] = true;
                        continue;
                    }

                    // EAST
                    visible = true;
                    for (int j = treeCols - 1; visible && currentCol < j; j--)
                        if (treeArray[currentRow, j] >= height)
                            visible = false;

                    if (visible)
                    {
                        visibleTreesSum++;
                        visibleTreesArray[currentRow, currentCol] = true;
                        continue;
                    }

                    // NORTH
                    visible = true;
                    for (int j = 0; visible && j < currentRow; j++)
                        if (treeArray[j, currentCol] >= height)
                            visible = false;

                    if (visible)
                    {
                        visibleTreesSum++;
                        visibleTreesArray[currentRow, currentCol] = true;
                        continue;
                    }

                    // SOUTH
                    visible = true;
                    for (int j = treeRows - 1; visible && currentRow < j; j--)
                        if (treeArray[j, currentCol] >= height)
                            visible = false;

                    if (visible)
                    {
                        visibleTreesSum++;
                        visibleTreesArray[currentRow, currentCol] = true;
                        continue;
                    }
                }

            Int64[,] treeScores = new Int64[treeRows, treeCols];
            for (int currentRow = 0; currentRow < treeRows; currentRow++)
                for (int currentCol = 0; currentCol < treeCols; currentCol++)
                {
                    var height = treeArray[currentRow, currentCol];
                    // w
                    var cnt = 0;
                    for (int j = currentCol - 1; j >= 0; j--)
                    {
                        cnt++;
                        if (treeArray[currentRow, j] >= height)
                            break;
                    }
                    treeScores[currentRow, currentCol] *= cnt;

                    // e
                    cnt = 0;
                    for (int j = currentCol + 1; j < treeRows; j++)
                    {
                        cnt++;
                        if (treeArray[currentRow,j] >= height)
                            break;
                    }
                    treeScores[currentRow, currentCol] *= cnt;

                    // n
                    cnt = 0;
                    for (int j = currentRow - 1; j >= 0; j--)
                    {
                        cnt++;
                        if (treeArray[j,currentCol] >= height)
                            break;
                    }
                    treeScores[currentRow, currentCol] *= cnt;

                    // s
                    cnt = 0;
                    for (int j = currentRow + 1; j < treeRows; j++)
                    {
                        cnt++;
                        if (treeArray[j,currentCol] >= height)
                            break;
                    }
                    treeScores[currentRow, currentCol] *= cnt;
                }
            // For displaying
            //for (int rowIdx = 0; rowIdx < treeRows; rowIdx += 1)
            //{
            //    for (int colIdx = 0; colIdx < treeCols; colIdx += 1)
            //    {
            //        Console.Write(treeArray[rowIdx, colIdx] + " ");
            //    }
            //    Console.Write("\n");
            //}

            //for (int rowIdx = 0; rowIdx < treeRows; rowIdx += 1)
            //{
            //    for (int colIdx = 0; colIdx < treeCols; colIdx += 1)
            //    {
            //        Console.Write(visibleTreesArray[rowIdx, colIdx] + " ");
            //    }
            //    Console.Write("\n");
            //} 


            for (int rowIdx = 0; rowIdx < treeRows; rowIdx += 1)
            {
                for (int colIdx = 0; colIdx < treeCols; colIdx += 1)
                {
                    Console.Write(treeScores[rowIdx, colIdx] +" ");
                }
                Console.Write("\n");
            }

            List<bool> part1 = visibleTreesArray.Cast<bool>().ToList();
            List<Int64> part2 = treeScores.Cast<Int64>().ToList();

            Console.WriteLine($"Part 1:{visibleTreesSum}");
            //Console.WriteLine($"Part 1:{part1.Select(val => val==true).Count()}");
            Console.WriteLine("Part 2:");
            Console.Write(part2.Max());
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

        private static bool Visible(int[,] trees, int row, int col, int height)
        {

            return false;
        }

    }
   
}