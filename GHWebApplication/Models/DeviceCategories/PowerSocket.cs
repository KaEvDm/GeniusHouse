using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class PowerSocket : Device
    {
        public bool IsOn { get; set; }

        public PowerSocket(Device dev) : base(dev)
        {
            this.IsOn = Convert.ToBoolean(DeviceInfoSerializer.RunString(dev.Info, "isOn"));
        }
    }
}
