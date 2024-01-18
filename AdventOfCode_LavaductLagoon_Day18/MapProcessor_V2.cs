using AdventOfCode_LavaductLagoon_Day18.Enums;
using AdventOfCode_LavaductLagoon_Day18.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_LavaductLagoon_Day18
{
    public class MapProcessor_V2
    {
        public static void UpdateMap(DigMap map, List<Instructions> instructions)
        {
            var currentRow = 0;
            var startX = 0;
            var startY = 0;

            var instructionsCounted = instructions.Count;

            AddRows(map, instructionsCounted);


            foreach (var instruction in instructions)
            {
                var longestRow = FindLongestRowLength(map);
                currentRow++;
                FillRowsWithDots(map, longestRow);
                DrawMap(map);


                if (instruction.Direction == DirectionEnum.MoveToRight)
                {
                    if (!(map.Map[currentRow].Count == longestRow))
                    {
                        for (int i = 0; i < longestRow; i++)
                        {
                            map.Map[currentRow].Add(".");
                        }
                    }

                    var symbolsAlreadyInARow = map.Map[currentRow].Count;

                    for (int steps = 0; steps < instruction.Steps; steps++)
                    {
                        if (map.Map[currentRow - 1].Count == 0) 
                        {
                            for (int i = 0; i < instruction.Steps; i++)
                            {
                                map.Map[currentRow - 1].Add("#");
                            }
                            break;
                        }

                        if(longestRow >= startY + steps)
                        {
                            map.Map[currentRow - 1][startY + 1 + steps] = "#";
                        }
                        else
                        {
                            map.Map[currentRow - 1].Add("#");
                        }
                    }

                    if(startY == 0)
                    {
                        startY += instruction.Steps-1;
                    }
                    else
                    {
                        startY += instruction.Steps;
                    }

                }

                if (instruction.Direction == DirectionEnum.MoveToLeft)
                {
                    if(instruction.Steps > startY)
                    {
                        var columnsToAdd = instruction.Steps - startY;
                        InsertColumnsToAllRows(map, columnsToAdd);
                        startY += columnsToAdd;
                    }
                    for(int steps = 0; steps <= instruction.Steps; steps++)
                    {
                        map.Map[startX][startY - steps] = "#";
                    }
                    
                    startY = startY - instruction.Steps;

                }

                if(instruction.Direction == DirectionEnum.MoveDown)
                {
                    AddRows(map, instruction.Steps);
                    for(int row = 0; row <= instruction.Steps; row++)
                    {
                        if (startY <= map.Map[currentRow + row].Count)
                        {
                            map.Map[currentRow - 1 + row][startY] = "#";
                        }
                        else
                        {
                            map.Map[currentRow + row].Add("#");
                        }
                        
                        startX = currentRow + row;
                    }
                    currentRow = startX-1;
                }

                if (instruction.Direction == DirectionEnum.MoveUp)
                {
                    if (instruction.Steps > startX)
                    {
                        var rowsToAdd = instruction.Steps - startY;
                        InsertRowsToTop(map, rowsToAdd);
                        startX += rowsToAdd;
                    }
                    for (int steps = 0; steps <= instruction.Steps; steps++)
                    {
                        map.Map[startX - steps][startY] = "#";
                    }
                    startX = startX - instruction.Steps;
                    currentRow = startX + 1;
                }

                
                DrawMap(map);
                Console.WriteLine();
            }
        }

        private static void InsertColumnsToAllRows(DigMap map, int columnsToAdd)
        {
            for(int i = 0; i < columnsToAdd; i++)
            {
                foreach(var row in map.Map)
                {
                    row.Insert(0, ".");
                }
            }
        }
        private static void FillRowsWithDots(DigMap map, int longestRow)
        {
            foreach (var row in map.Map)
            {
                if(row.Count == 0)
                {
                    for(int i = 0; i < longestRow; i++)
                    {
                       
                        row.Add(".");
                    }
                }
                for(int i = 0 ; i < longestRow ; i++)
                {
                    if(row.Count == longestRow)
                    {
                        break;
                    }
                    else
                    {
                        row.Add(".");
                    }
                }
            }
        }

        public static int FindLongestRowLength(DigMap map)
        {
            if (map.Map.Count == 0)
            {
                return 0;
            }
            else
            {
                return map.Map.OrderByDescending(row => row.Count).FirstOrDefault().Count();
            }
        }
        private static void AddRows(DigMap map, int rowsToAdd)
        {
            var addedCount = 0;

            while (addedCount < rowsToAdd)
            {
                map.Map.Add(new List<string>());
                addedCount++;
            }
        }

        private static void InsertRowsToTop(DigMap map, int rowsToAdd)
        {
            var addedCount = 0;

            while (addedCount < rowsToAdd)
            {
                map.Map.Insert(0, new List<string>());
                addedCount++;
            }
        }

        private static void DrawMap(DigMap map)
        {
            Console.WriteLine();
            for (int i = 0; i < map.Map.Count; i++)
            {
                string line = "";
                for (int j = 0; j < map.Map[i].Count; j++)
                {
                    line += map.Map[i][j];
                }
                Console.WriteLine(line);
            }
        }
    }
}
