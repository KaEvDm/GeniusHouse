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
    }
}
