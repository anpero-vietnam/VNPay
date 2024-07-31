using Anpero.PaymentHelper.Control.Alepay;
using Anpero.PaymentHelper.Control.QR;
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

        public TransactionDetailModel? ProcessCallBackData(string token)
        {
            switch (PaymentConfig.PaymentCode)
            {
                case PaymentCode.Alepay:
                    AlepayCheckout client = new AlepayCheckout(IsTest, PaymentConfig);
                    return client.GetTransactionDetail(token);
                default:
                    return null;
            }
            
        }
        public CheckOutResultModel? GetCheckoutUrl(OrderModel data)
        {         
            switch (PaymentConfig.PaymentCode)
            {
                case PaymentCode.Alepay:
                    AlepayCheckout client = new AlepayCheckout(IsTest, PaymentConfig);
                    return client.GetCheckoutUrl(data);              
                default:
                    return null;
            }
        }
        public CheckOutResultModel? GetQRCodeData(string bankAccountNumber, string bankId, string message, string amount, CurrencyCode currencyCode)
        {
            QRCodeGeneral qRCodeGeneral = new QRCodeGeneral(bankAccountNumber, bankId, message, amount, currencyCode); 
            return qRCodeGeneral.Build();            
        }
    }
}
