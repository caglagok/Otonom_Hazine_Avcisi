using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolab1.Core
{
    public abstract class Engel
    {
      
        public int Boyut { get; set; }
        public string Name { get; set; }

        public Engel( int boyut, string name)
        {
          
            Boyut = boyut;
            Name = name;
        }
    }
}
