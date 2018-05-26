using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class AirConditioner : Device
    {
        public int Mode { get; set; }
        public double Temperature { get; set; }
        public double TemperatureSensorReadings { get; set; }
        public bool IsWater { get; set; }

        public AirConditioner(Device dev) : base(dev)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberGroupSeparator = ".";
            this.Mode = Convert.ToInt32(DeviceInfoSerializer.RunString(dev.Info, "mode"));
            this.Temperature = Convert.ToDouble(DeviceInfoSerializer.RunString(dev.Info, "temperature"), provider);
            this.TemperatureSensorReadings = Convert.ToDouble(DeviceInfoSerializer.RunString(dev.Info, "temperatureSensorReadings"), provider);
            this.IsWater = Convert.ToBoolean(DeviceInfoSerializer.RunString(dev.Info, "isWater"));
        }
    }
}
