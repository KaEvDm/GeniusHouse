using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GHWebApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GHWebApplication.TasksRunAsync;

namespace GHWebApplication
{
    public class Startup
    {
        //Этот метод вызывается во время исполнения.
        //Используется для добавления сервисов в контейнер
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            string connectionString = "Host=localhost;Port=5432;Database=DevicesDb;Username=postgres;Password=123";
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
            services.AddSingleton<ITaskInvoke, UpdateDatabase>();
            services.AddSingleton<ITaskInvoke, Neuron>();
            services.AddSingleton<IHostedService, TaskRunner>();
            
        }

        //Этот метод вызывается во время исполнения. 
        //Используется для конфигурации конвейера HTTP-запроса 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            //});
        }
    }
}
