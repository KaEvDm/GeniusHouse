using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GHWebApplication.Models
{
    public static class DeviceInfoSerializer
    {

        public static List<Device> ConvertJsonInDevices(string json, ApplicationContext db)
        {
            var devices = new List<Device>();
            int i = 0;
            var str = new List<string>();

            do
            {
                string strDev = "";

                do
                {
                    i++;
                    strDev += json[i];
                } while (json[i] != '}');

                str.Add(strDev);
            } while (json[i + 1] != ']');

            foreach (var item in str)
            {
                var resultStr = item.Trim(',');
                devices.Add(ConvertJsonInDevice(resultStr, db));
            }

            return devices;
        }


        public static Device ConvertJsonInDevice(string json, ApplicationContext db)
        {
            var dev = db.Devices.FirstOrDefault(x => x.Name == RunString(json, "name"));
            dev.Power = Convert.ToBoolean(RunString(json, "power"));
            dev.Company = RunString(json, "company");
            var info = DeleteDeviceFields(json);

            dev.Info = info;

            return dev;
        }

        public static string DeleteDeviceFields(string str)
        {
            str = DeleteField(str, "id");
            //DeleteField(str, "category");
            //DeleteField(str, "room");
            str = DeleteField(str, "name");
            str = DeleteField(str, "company");
            str = DeleteField(str, "power");

            str = DeleteField(str, "isConect");

            return str;
        }

        public static string DeleteField(string str, string fieldName)
        {
            if (str.Contains(fieldName))
            {
                var startField = str.IndexOf(fieldName);
                var endField = str.IndexOf(',', startField);

                if (endField == -1) endField = str.IndexOf('}', startField) - 1;

                var lengthField = endField - startField;

                str = str.Remove(startField - 1, lengthField + 2);
            }

            return str;
        }

        public static string RunString(string str, string fieldName)
        {
            if (str.Contains(fieldName))
            {
                var i = str.IndexOf(fieldName) + fieldName.Length + "\":".Length;

                string value = "";

                while (str[i] != ',' && str[i] != '}')
                {
                    value += str[i];
                    i++;
                }

                value = value.Trim('\"');

                return value;
            }
            else
            {
                return "Error";
            }
        }

        public static string ChangeFieldFromInfo<T>(string str, string fieldName, T value)
        {

            var start = str.IndexOf(fieldName) + fieldName.Length + "\":".Length;
            var end = str.IndexOf(',', start);
            str = str.Remove(start, end - start);
            var result = Convert.ToString(value, new NumberFormatInfo { NumberGroupSeparator = "." });
            result = result.Remove(5);
            str = str.Insert(start, result);

            return str;
        }
    }
}

