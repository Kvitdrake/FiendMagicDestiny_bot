using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiendMagicDestiny_bot
{
    internal class TaroArcans
    {

        public string Name { get; set; }
        public string DescriptionM { get; set; }
        public string DescriptionG { get; set; }
        public Dictionary<string, string> Combinations { get; set; }
        public static short[] Arcs;

        public TaroArcans(string name, string descriptionM, string descriptionG)
        {
            Name = name;
            DescriptionM = descriptionM;
            DescriptionG = descriptionG;
            Combinations = new Dictionary<string, string>();
        }
    }
}