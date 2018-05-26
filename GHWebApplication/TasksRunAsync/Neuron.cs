using GHWebApplication.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceCategories;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;

namespace GHWebApplication.TasksRunAsync
{
    public class Neuron : ITaskInvoke
    {
        public IServiceProvider _provider;

        public double PeriodMS => 1000 * 60 * 30; //30 мин

        private const double AbsoluteZeroKelvin = 273.15;

        public Neuron(IServiceProvider provider)
        {
            _provider = provider;

            NeuronTraining.LastTemperatureOutside = GetTemperatureOutside();
            NeuronTraining.LastTemperatureInside = GetTemperatureInside();

            Init(NeuronTraining.LastTemperatureOutside, NeuronTraining.LastTemperatureInside);
        }

        public void Init(double OutsideTemperature, double InsideTemperature)
        {
            //Выбираем рекомендуемую температуру
            var RecommendedTemperature = NeuronTraining.СalculateRecommendedTemperature();

            var InsideTemperature0 = InsideTemperature - 0.01;
            var InsideTemperature1 = InsideTemperature + 0.01;
            var OutsideTemperature0 = OutsideTemperature + 0.5;
            var OutsideTemperature1 = OutsideTemperature - 0.7;


            // Решаем систему линейных уравнений
            // { Outside0 * Weight[0] +  Inside0 * Weight[1] = Expected0
            // { Outside1 * Weight[0] +  Inside1 * Weight[1] = Expected1
            // Методом Крамера
            double delta = OutsideTemperature0 * InsideTemperature1 - OutsideTemperature1 * InsideTemperature0;
            double delta0 = RecommendedTemperature * InsideTemperature1 - InsideTemperature0 * RecommendedTemperature;
            double delta1 = OutsideTemperature0 * RecommendedTemperature - RecommendedTemperature * OutsideTemperature1;
            NeuronTraining.Weight[0] = delta0 / delta;
            NeuronTraining.Weight[1] = delta1 / delta;

        }

       

        public Task Work()
        {
            var InsideTemperature = GetTemperatureInside();
            var OutsideTemperature = GetTemperatureOutside();

            var ResultTemperature = OutsideTemperature * NeuronTraining.Weight[0] + InsideTemperature * NeuronTraining.Weight[1];

            NeuronTraining.LastTemperatureInside = InsideTemperature;
            NeuronTraining.LastTemperatureOutside = OutsideTemperature;

            //отправляем результаты
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                var temperatureDevices = context.Devices.Where(
                    z => z.Category == "AirConditioner" || z.Category == "Heater").ToList();
                if(temperatureDevices != null)
                {
                    foreach (var item in temperatureDevices)
                    {
                        item.Info = DeviceInfoSerializer.ChangeFieldFromInfo<double>(item.Info, "temperature", ResultTemperature);

                        var uri = "http://localhost:52131/api/device/Update/" + item.Id.ToString();
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                        request.Method = "PUT";
                        request.ContentType = "application/json";

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            var json = JsonConvert.SerializeObject(item);
                            streamWriter.Write(json);
                        }

                        WebResponse response = request.GetResponse();
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        public double GetTemperatureOutside()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "http://api.openweathermap.org/data/2.5/weather?id=1508291&APPID=ce2124ddd35bd7afd4eff844857c517d");
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string json = streamReader.ReadToEnd();
                var weather = Weather.FromJson(json);

                return weather.Main.Temp - AbsoluteZeroKelvin;
            }
        }

        public double GetTemperatureInside()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                var dev = context.Devices.FirstOrDefault(z => z.Category == "AirConditioner" || z.Category == "TemperatureSensor");
                if (dev != null)
                {
                    var s = DeviceInfoSerializer.RunString(dev.Info, "temperatureSensorReadings");
                    var a = Convert.ToDouble(s, new NumberFormatInfo { NumberGroupSeparator = "." });
                    return a;
                }
                else return 25;
            }
        }

        public Task Invoke()
        {
            return Work();
        }
    }
}
