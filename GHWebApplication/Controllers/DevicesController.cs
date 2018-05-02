using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GHWebApplication.Models;
using System;
using System.Reflection;

namespace HelloAngularApp.Controllers
{
    [Route("api/[controller]/[action]/")]
    public class DeviceController : Controller
    {
        ApplicationContext db;
        public DeviceController(ApplicationContext context)
        {
            db = context;
            if (!db.Devices.Any())
            {
                db.Devices.Add(new Device { Name = "iPhone X", Company = "Apple" });
                db.Devices.Add(new Device { Name = "Galaxy S8", Company = "Samsung" });
                db.Devices.Add(new Device { Name = "Pixel 2", Company = "Google" });
                db.SaveChanges();
            }
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

        public IEnumerable<Device> GetSort(string room = "", string category = "")
        {
            if (category == null) category = "";
            if (room == null) room = "";
            return db.Devices.Where(x => x.Room.Contains(room)).Where(x => x.Category.Contains(category)).ToList();
        }

        [HttpPost]
        public IActionResult AddNew([FromBody]Device dev)
        {
            if (ModelState.IsValid)
            {
                //============кусок который должен будет взаимодействовать с микроконтроллером========
                //"test." - это namespace в котором лежит dev
                //dev.Category - ну к примеру "Lamp"
                //var type = Type.GetType("DeviceCategories." + dev.Category);

                //ConstructorInfo ctor = type.GetConstructor(new Type[] { dev.GetType() });
                //var result = ctor.Invoke(new object[] { dev });
                //============кусок который должен будет взаимодействовать с микроконтроллером========

                db.Devices.Add(dev);
                db.SaveChanges();
                return Ok(dev);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Device dev)
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