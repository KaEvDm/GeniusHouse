using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GHWebApplication.Models;

namespace HelloAngularApp.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        ApplicationContext db;
        public ProductController(ApplicationContext context)
        {
            db = context;
            if (!db.Devices.Any())
            {
                db.Devices.Add(new Device { Name = "iPhone X", Company = "Apple", Price = 79900 });
                db.Devices.Add(new Device { Name = "Galaxy S8", Company = "Samsung", Price = 49900 });
                db.Devices.Add(new Device { Name = "Pixel 2", Company = "Google", Price = 52900 });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Device> Get()
        {
            return db.Devices.ToList();
        }

        [HttpGet("{id}")]
        public Device Get(int id)
        {
            Device product = db.Devices.FirstOrDefault(x => x.Id == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Device product)
        {
            if (ModelState.IsValid)
            {
                db.Devices.Add(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Device product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Device product = db.Devices.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Devices.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
}