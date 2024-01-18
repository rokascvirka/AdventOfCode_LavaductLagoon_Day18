using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_LavaductLagoon_Day18.Models
{
    public class DigMap
    {
        public List<List<string>> Map{ get; set; }
        public List<List<string>> UpdatedMap { get; set; }
        public DigMap()
        {
            Map = new List<List<string>>();
            UpdatedMap = new List<List<string>>();
        }
    }
}
