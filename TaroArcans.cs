using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiendMagicDestiny_bot
{
    internal class TaroArcans
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaroArcans> arcans { get; set; }
        public short[] Arcs;
    }
}
