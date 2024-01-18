using AdventOfCode_LavaductLagoon_Day18.Models;
using System.Reflection;

namespace AdventOfCode_LavaductLagoon_Day18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var FILE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "FakeData.txt");

            var instructions = FileReader.ReadFromFile(FILE_PATH);
            var map = new DigMap();


            MapProcessor_V2.UpdateMap(map, instructions);

            for (int i = 0; i < map.Map.Count; i++)
            {
                string line = "";
                for (int j = 0; j < map.Map[0].Count; j++)
                {
                    line += map.UpdatedMap[i][j];
                }
                Console.WriteLine(line);
            }


            Console.WriteLine("Done!");
        }
    }
}