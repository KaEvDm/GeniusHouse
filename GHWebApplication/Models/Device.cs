using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHWebApplication.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Room { get; set; }
        public string Name { get; set; }
        public string Company { get; set; } 
        public bool Power { get; set; }
        public string Info { get; set; }

        public Device() { }

        public Device(Device dev)
        {
            this.Id = dev.Id;
            this.Category = dev.Category;
            this.Room = dev.Room;
            this.Name = dev.Name;
            this.Company = dev.Company;
            this.Power = dev.Power;
            this.Info = dev.Info;
        }
    }
}
