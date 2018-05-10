using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GHWebApplication
{
    public static class UpdateDatabase
    {
       

        public static string getRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/api/lamps");

            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        
    }
}
