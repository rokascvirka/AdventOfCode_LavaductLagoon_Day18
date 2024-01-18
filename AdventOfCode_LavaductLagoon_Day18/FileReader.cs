using AdventOfCode_LavaductLagoon_Day18.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_LavaductLagoon_Day18
{
    public static class FileReader
    {
        public static List<Instructions> ReadFromFile(string path)
        {
            List<Instructions> instructions = new List<Instructions>();

            using ( StreamReader sr = new StreamReader(path))
            {
                var text = sr.ReadToEnd().Split(Environment.NewLine);
                foreach(var line in text)
                {
                    var content = line.Split(" ");
                    var direction = content[0];
                    var steps = int.Parse(content[1]);
                    var color = content[2].Replace("(", "").Replace(")", "");

                    instructions.Add(new Instructions(direction, steps, color));
                }
            }
            return instructions;
        }
    }
}
