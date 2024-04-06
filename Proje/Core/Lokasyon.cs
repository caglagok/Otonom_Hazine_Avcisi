using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolab1.Core
{
    public class Lokasyon
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Lokasyon(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
