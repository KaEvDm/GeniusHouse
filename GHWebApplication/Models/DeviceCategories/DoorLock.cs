using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;

namespace DeviceCategories
{
    public class DoorLock : Device
    {
        public bool IsOpen { get; set; }

        public DoorLock(Device dev) : base(dev)
        {
            this.IsOpen = Convert.ToBoolean(DeviceInfoSerializer.RunString(dev.Info, "isOpen"));
        }
    }
}
