using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class Heater : Device
    {
        public double Temperature { get; set; }

        public Heater(Device dev) : base(dev)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberGroupSeparator = ".";
            this.Temperature = Convert.ToDouble(DeviceInfoSerializer.RunString(dev.Info, "temperature"), provider);
        }
    }
}
