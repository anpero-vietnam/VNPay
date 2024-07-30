
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper
{
    internal class HttpHelper<T>
    {
        private static readonly HttpClient client = new HttpClient();
        public static T? PostJson(string url, object paramObject)
        {

            string json = "";
            ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var dataJson = JsonSerializer.Serialize(paramObject);
            var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            if (json != "")
            {
                try
                {
                    return JsonSerializer.Deserialize<T?>(json);
                }
                catch 
                {
                    return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }
    }
}
