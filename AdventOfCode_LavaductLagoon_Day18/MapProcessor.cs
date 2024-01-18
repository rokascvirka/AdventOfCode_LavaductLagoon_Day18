using AdventOfCode_LavaductLagoon_Day18.Enums;
using AdventOfCode_LavaductLagoon_Day18.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_LavaductLagoon_Day18
{
    public class MapProcessor
    {
        public static void UpdateMap(DigMap map, List<Instructions> instructions)
        {
            var currentRow = map.Map.Count - 1;
            var longestRow = 0;
            var currentStepX = 0;
            var currentStepY = 0;


            foreach(var instruction in instructions)
            {
                currentRow ++;
                map.Map.Add(new List<string>());

                if (instruction.Direction == DirectionEnum.MoveToRight)
                {
                    var count = 0;
                    for(int column = 0; column < currentStepX; column++)
                    {
                        map.Map[currentRow].Add(".");
                    }

                    while(count < instruction.Steps)
                    {
                        map.Map[currentRow].Add("#");
                        count++;
                    }
                    currentStepY = currentStepY + count;
                    currentStepX = currentRow;

                    longestRow = ExtendRows(map, longestRow);
                    DrawMap(map);
                }

                if(instruction.Direction == DirectionEnum.MoveToLeft)
                {
                    if(currentStepY > instruction.Steps)
                    {
                        var extension = instruction.Steps - currentStepY;
                        if (extension > 0)
                        {
                            foreach (var row in map.Map)
                            {
                                for (var i = 0; i < extension; i++)
                                {
                                    row.Insert(0, ".");
                                }
                            }
                        }
                    }
                    //map.Map.Remove(map.Map.Last());

                    for(int column = currentStepY - 1; column >= 0; column--)
                    {
                        map.Map[currentRow][column] = "#";
                    }

                    longestRow = ExtendRows(map, longestRow);
                    DrawMap(map);

                }

                if (instruction.Direction == DirectionEnum.MoveDown)
                {
                    for (int row = currentRow; row < instruction.Steps; row++)
                    {
                        map.Map.Add(new List<string>());

                        AddFirstPartOfTheRow(map, currentRow + row, longestRow);
                    }
                    map.Map.RemoveAt(map.Map.Count - 1);
                    longestRow = ExtendRows(map, longestRow);

                    for (int row = currentRow; row < map.Map.Count; row++)
                    {
                        map.Map[row][currentStepY] = "#";
                    }
                    currentRow += instruction.Steps - 1;
                    DrawMap(map);

                }


                if (instruction.Direction == DirectionEnum.MoveUp)
                {
                    if(currentStepX < instruction.Steps)
                    {
                        var difference = currentStepX - instruction.Steps;
                        for( var i = 0;i < difference; i++)
                        {
                            map.Map.Insert(0,new List<string>());
                            AddFirstPartOfTheRow(map, 0, map.Map.OrderByDescending(l => l.Count).FirstOrDefault().Count());
                            currentRow++;
                        }
                    }

                    for (int row = currentRow - 1; row >= 0; row--)
                    {
                        map.Map[row][currentStepY] = "#";
                    }

                    longestRow = ExtendRows(map, longestRow);
                    DrawMap(map);

                }


            }
        }
        private static int ExtendRows(DigMap map, int longestRow)
        {
            var longestList = map.Map.OrderByDescending(l => l.Count).FirstOrDefault().Count();
            if (longestList > longestRow)
            {
                longestRow = longestList;
            }

            foreach (var row in map.Map)
            {
                if (row.Count < longestRow)
                {
                    for (var i = row.Count; i < longestRow; i++)
                    {
                        row.Add(".");
                    }
                }
            }

            return longestRow;
        }

        private static void AddFirstPartOfTheRow(DigMap map,int currentRow, int y)
        {
           for(var column = 0;  column <= y; column++)
            {
                map.Map[currentRow].Add(".");
            }
        }



        private static int FindLongestList(DigMap map)
        {
            return map.Map.OrderByDescending(l => l.Count).FirstOrDefault().Count();
        }

        private static void DrawMap(DigMap map)
        {
            Console.WriteLine();
            for (int i = 0; i < map.Map.Count; i++)
            {
                string line = "";
                for (int j = 0; j < map.Map[i].Count-1; j++)
                {
                    line += map.Map[i][j];
                }
                Console.WriteLine(line);
            }
        }
    }
}
