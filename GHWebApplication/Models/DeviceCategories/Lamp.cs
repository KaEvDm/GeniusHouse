using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class Lamp : Device
    {
        public int Brightness { get; set; }

        public Lamp(Device dev) : base(dev)
        {
            this.Brightness = Convert.ToInt32(DeviceInfoSerializer.RunString(dev.Info, "brightness"));
        }
    }
}
