using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    public struct Seashell
    {
        public readonly Point2D Position;
        public readonly SideColor Color;

        public Seashell(Point2D position, SideColor color)
        {
            Position = position;
            Color = color;
        }
    }

    public class TBBWorldSettings
    {
        static Point2D[] seashells = new Point2D[]
        {
            new Point2D(140, -80),
            new Point2D(140, -90),
            new Point2D(130, -80),
            new Point2D(130, -25),
            new Point2D(130, -55),
            new Point2D(80, -25),
            new Point2D(80, -55),
            new Point2D(80, -85),
            new Point2D(60, -45),
            new Point2D(30, -65),
            new Point2D(0, -55),
            new Point2D(0, -85),
        };

        static string[] allPatterns = new string[]
        {
            "GGGWW---GVWW",
            "GWGGW---GGWW",
            "GWGGWGW--G--",
            "VWVGGGW--W--",
            "GWWGGGVW----",
        };

        static Dictionary<char, SideColor> converter = new Dictionary<char, SideColor>()
        {
            {'W', SideColor.Any },
            {'G', SideColor.Green },
            {'V', SideColor.Violet},
        };

        static Dictionary<char, char> inverter = new Dictionary<char, char>()
        {
            {'W', 'W'},
            {'G', 'V'},
            {'V', 'G'},
        };

        static SideColor GetColor(int patternIndex, int shellIndex)
        {
            var pattern = allPatterns[patternIndex];
            var color = pattern[shellIndex % pattern.Length];
            return shellIndex >= pattern.Length ? converter[inverter[color]] : converter[color];
        }

        static bool IsEmpty(int configurationIndex, int shellIndex)
        {
            var configuration = allPatterns[configurationIndex];
            return !converter.ContainsKey(configuration[shellIndex % configuration.Length]);
        }

        public static IEnumerable<Seashell> GenerateSeashellsLayout(ushort seed)
        {
            var allShells = seashells
                .Union(seashells.Select(x => new Point2D(-x.X, x.Y)))
                .Distinct();
            
            int config = seed % allPatterns.Length;

            foreach (var ie in allShells.Enumerate())
                if (!IsEmpty(config, ie.Index))
                    yield return new Seashell(ie.Item, GetColor(config, ie.Index));
        }
    }
}
