using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHWebApplication.Models
{
    public static class DeviceInfoSerializer
    {
        public static int Run(string str, string fieldName)
        {
            if (str.Contains(fieldName))
            {
                var i = str.IndexOf(fieldName) + fieldName.Length + " = ".Length;
                string value = "";

                while (str[i] != ';')
                {
                    value += str[i];
                    i++;
                }
                return Convert.ToInt32(value);
            }
            else
            {
                return -1;
            }
        }
    }
}
