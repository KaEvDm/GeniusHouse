using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GHWebApplication.Models;

namespace HelloAngularApp.Controllers
{
    [Route("api/devices")]
    public class DeviceController : Controller
    {
        ApplicationContext db;
        public DeviceController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Device> Get()
        {
            return db.Devices.ToList();
        }

        [HttpGet("{id}")]
        public Device Get(int id)
        {
            Device dev = db.Devices.FirstOrDefault(x => x.Id == id);
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