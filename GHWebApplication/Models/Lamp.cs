using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHWebApplication.Models
{
    public class Lamp : Device
    {
        public int Brightness { get; set; }
        public int[] ColorRGB { get; set; }

        public Lamp()
        {
            ColorRGB = new int[3];
        }
    }
}
