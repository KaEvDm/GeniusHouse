using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GHWebApplication.Models;
using System;
using System.Reflection;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace HelloAngularApp.Controllers
{
    [Route("api/[controller]/[action]/")]
    public class DeviceController : Controller
    {
        ApplicationContext db;
        public DeviceController(ApplicationContext context)
        {
            db = context;
            //if (!db.Devices.Any())
            //{
            //    db.Devices.Add(new Device { Name = "iPhone X", Company = "Apple" });
            //    db.Devices.Add(new Device { Name = "Galaxy S8", Company = "Samsung" });
            //    db.Devices.Add(new Device { Name = "Pixel 2", Company = "Google" });
            //    db.SaveChanges();
            //}
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

                string requestUri = "http://localhost/api/lamp/connectDevice?name=" + dev.Name;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

                WebResponse response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if(result == "device is connected" || result == "device is already connected")
                    {
                        db.Devices.Add(dev);
                        db.SaveChanges();
                        return Ok(dev);
                    }
                    else
                    {
                        return BadRequest("device is not found");
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Device dev)
        {
            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/api/lamp/command");
                request.Method = "PUT";
                request.ContentType = "application/json";

                var type = Type.GetType("DeviceCategories." + dev.Category);
                ConstructorInfo ctor = type.GetConstructor(new Type[] { dev.GetType() });
                var resultDev = ctor.Invoke(new object[] { dev });

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(resultDev);
                    streamWriter.Write(json);
                }

                WebResponse response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

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
                string requestUri = "http://localhost/api/lamp/disconnectDevice?name=" + dev.Name;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

                WebResponse response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (result == "device is disconnected" || result == "device is already disconnected")
                    {
                        db.Devices.Remove(dev);
                        db.SaveChanges();
                        return Ok(dev);
                    }
                    else
                    {
                        return BadRequest("device is not found");
                    }
                }
            }
            return Ok(dev);
        }
    }
}