using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHWebApplication.TasksRunAsync
{
    public static class NeuronTraining
    {
        public static double[] Weight = new double[2];

        public static double LastTemperatureOutside = 0;
        public static double LastTemperatureInside = 0;

        public static void Training(double RecommendedTemperature = -100)
        {
            //Выбираем рекомендуемую температуру
            if (RecommendedTemperature <= -100)
                RecommendedTemperature = СalculateRecommendedTemperature();

            var PredictedTemperature = LastTemperatureOutside * Weight[0] + LastTemperatureInside * Weight[1];

            double deltaWeight = (2 / Math.PI) * Math.Atan(RecommendedTemperature - PredictedTemperature);

            Weight[0] += deltaWeight;
            Weight[1] += deltaWeight;
        }

        public static double СalculateRecommendedTemperature()
        {
            var Now = new DateTime();
            Now = DateTime.Now;
            if (Now.Hour >= 23 && Now.Hour < 7) return 18;
            else if (Now.Hour >= 7 && Now.Hour < 12) return 23;
            else if (Now.Hour >= 12 && Now.Hour < 20) return 20;
            else if (Now.Hour >= 20 && Now.Hour < 23) return 23;
            else return 20; //среднее
        }
    }
}
