using Anpero.PaymentHelper.Control.Alepay;
using Anpero.PaymentHelper.Model;
using Anpero.PaymentHelper.Model.Alepay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Anpero.PaymentHelper
{
    public class AllPayment : IAllPayment
    {
        public readonly PaymentConfig PaymentConfig;
        public readonly bool IsTest;
        public AllPayment(PaymentConfig paymentConfig, bool isTest)
        {
            PaymentConfig = paymentConfig;            
            IsTest = isTest;    
        }

        public Task<dynamic> GetCallBackData(string token)
        {


            throw new NotImplementedException();
        }
        public string? GetRedirectUrl(PaymentConfig config, OrderModel data)
        {         
            switch (config.PaymentCode)
            {
                case "AL":
                    AlepayCheckout client = new AlepayCheckout(IsTest, config);
                    return client.GetRedirectUrl(data);
                default:
                    return string.Empty;
            }
        }
    }
}
