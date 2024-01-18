using AdventOfCode_LavaductLagoon_Day18.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_LavaductLagoon_Day18.Models
{
    public class Instructions
    {
        public DirectionEnum Direction { get; set; }
        public int Steps { get; set; }
        public string Color { get; set; }

        public Instructions(string direction, int steps, string color)
        {
            Direction = GetDirection(direction);
            Steps = steps;
            Color = color;
        }

        private static DirectionEnum GetDirection(string direction)
        {
            switch (direction)
            {
                case "R":
                    return DirectionEnum.MoveToRight;
                case "L":
                    return DirectionEnum.MoveToLeft;
                case "D":
                    return DirectionEnum.MoveDown;
                case "U":
                    return DirectionEnum.MoveUp;
                default:
                    return DirectionEnum.Fail;
            }
        }

    }
}
