using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GHWebApplication.Models;

namespace HelloAngularApp.Controllers
{
    [Route("api/[controller]/[action]/")]
    public class DeviceController : Controller
    {
        ApplicationContext db;
        public DeviceController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Device> GetAll()
        {
            return db.Devices.ToList();
        }

        [HttpGet("{id:int}")]
        public Device GetOne(int id)
        {
            Device dev = db.Devices.FirstOrDefault(x => x.Id == id);
            return dev;
        }

        //http://localhost:52131/api/device/GetSort?room=handheld&category=phone
        public IEnumerable<Device> GetSort(string room, string category)
        {
            List<Device> dev = new List<Device>();

            if(room != null)
            {
                dev = db.Devices.Where(x => x.Room == room).ToList();
                if (category != null)
                {
                    dev = dev.Where(x => x.Category == category).ToList();
                }
            }
            else
            {
                if(category != null)
                {
                    dev = db.Devices.Where(x => x.Category == category).ToList();
                }
            }

            return dev;
            
        }

        [HttpPost]
        public IActionResult Post([FromBody]Device dev)
        {
            if (ModelState.IsValid)
            {
                db.Devices.Add(dev);
                db.SaveChanges();
                return Ok(dev);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Device dev)
        {
            if (ModelState.IsValid)
            {
                db.Update(dev);
                db.SaveChanges();
                return Ok(dev);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Device dev = db.Devices.FirstOrDefault(x => x.Id == id);
            if (dev != null)
            {
                db.Devices.Remove(dev);
                db.SaveChanges();
            }
            return Ok(dev);
        }
    }
}