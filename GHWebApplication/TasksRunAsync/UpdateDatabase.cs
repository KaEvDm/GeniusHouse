using GHWebApplication.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GHWebApplication.TasksRunAsync
{
    public class UpdateDatabase : ITaskInvoke
    {

        public IServiceProvider _provider;

        public double PeriodMS => 1000 * 30; //30 сек

        public UpdateDatabase(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task GetRequest(string devCategory)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/api/" + devCategory + "/get");

            WebResponse response = request.GetResponse();
            using(var scope = _provider.CreateScope())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                        var devices = DeviceInfoSerializer.ConvertJsonInDevices(reader.ReadToEnd(), context);
                        foreach (var item in devices)
                        {
                            item.Id = context.Devices.FirstOrDefault(x => x.Name == item.Name).Id;
                            context.Update(item);
                            context.SaveChanges();
                        }

                        return null;
                    }
                }
            }
        }

        public Task Invoke()
        {
            var devices = new List<Device>();
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                devices = context.Devices.ToList();
            }

            var devCategories = new List<String>();
            foreach (var item in devices)
            {
                if (!devCategories.Contains(item.Category))
                {
                    devCategories.Add(item.Category);
                }
            }

            foreach (var item in devCategories)
            {
                GetRequest(item);
            }

            return null;
        }
    }
}
