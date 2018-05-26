using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class MoveSensor : Device
    {
        public bool IsMove { get; set; }
        public bool IsSecureMod { get; set; }

        public MoveSensor(Device dev) : base(dev)
        {
            this.IsMove = Convert.ToBoolean(DeviceInfoSerializer.RunString(dev.Info, "isMove"));
            this.IsSecureMod = Convert.ToBoolean(DeviceInfoSerializer.RunString(dev.Info, "isSecureMod"));
        }
    }
}
