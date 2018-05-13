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
        //public ILogger<UpdateDatabase> _log;

        public double PeriodMS => 1000*30;

        public UpdateDatabase(IServiceProvider provider, ILogger<UpdateDatabase> log)
        {
            _provider = provider;
            //_log = log; 
        }

        public Task GetRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/api/lamp/get");

            WebResponse response = request.GetResponse();
            using(var scope = _provider.CreateScope())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                        //var divD = context.Devices.Where(z => z!= null).ToList();
                        var devices = DeviceInfoSerializer.ConvertJsonInDevices(reader.ReadToEnd(), context);
                        foreach (var item in devices)
                        {
                            item.Id = context.Devices.FirstOrDefault(x => x.Name == item.Name).Id;
                            context.Update(item);
                            context.SaveChanges();
                        }


                        //_log.LogInformation(reader.ReadToEnd());


                        return null;
                    }
                }
            }
        }

        public Task Invoke()
        {
            return GetRequest();
        }
    }
}
