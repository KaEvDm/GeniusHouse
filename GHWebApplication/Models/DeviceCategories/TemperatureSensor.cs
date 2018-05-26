using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class TemperatureSensor : Device
    {
        public double TemperatureSensorReadings { get; set; }

        public TemperatureSensor(Device dev) : base(dev)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberGroupSeparator = ".";
            this.TemperatureSensorReadings = Convert.ToDouble(DeviceInfoSerializer.RunString(dev.Info, "temperatureSensorReadings"), provider);
        }
    }
}
