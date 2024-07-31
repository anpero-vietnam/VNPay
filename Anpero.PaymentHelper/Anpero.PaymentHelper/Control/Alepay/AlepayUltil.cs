using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Control.Alepay
{

    internal class AlepayUltil
    {
        public string GetSignature(object obj, string checkSumKey)
        {
            var param = string.Empty;
            if (obj != null)
            {
                var propertyInfo = obj.GetType().GetProperties().OrderBy(x => x.Name);

                bool isFirstParam = true;
                foreach (var prop in propertyInfo)
                {
                    string propName = prop.Name;
                    var val = prop.GetValue(obj, null);
                    if (val != null && !propName.Equals("signature", StringComparison.OrdinalIgnoreCase))
                    {
                        if (propName.Equals("allowDomestic", StringComparison.OrdinalIgnoreCase) || propName.Equals("installment", StringComparison.OrdinalIgnoreCase))
                        {
                            param += (isFirstParam ? "" : "&") + propName + "=" + val.ToString().ToLower();
                        }
                        else
                        {
                            param += (isFirstParam ? "" : "&") + propName + "=" + val.ToString();
                        }

                        isFirstParam = false;
                    }
                }
            }

            return ComputeHMacSha256(param, checkSumKey).ToLower();
        }
        private string ComputeHMacSha256(string text, string key)
        {
            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            }
        }
    }

}
