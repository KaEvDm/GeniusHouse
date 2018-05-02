using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class Сonditioner : Device
    {
        public int Temperature { get; set; }

        public Сonditioner(Device dev)
        {
            this.Temperature = DeviceInfoSerializer.RunNumber(dev.Info, "Brightness");
        }
    }
}
