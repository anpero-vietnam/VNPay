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
        /// <summary>
        /// Return QR data 
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="bankId"></param>
        /// <param name="message"></param>
        /// <param name="amount"></param>
        /// <param name="currency">Indicates the currency code of the transaction.</param>
        /// <param name="service">[ISO 18245] Retail financial services—Merchant category codes</param>
        /// <param name="countryCode"></param>
        /// <param name="guid">Globally Unique  Identifier</param>
        /// <returns></returns>
        public CheckOutResultModel? GetQRCodeData(string bankAccount, string bankId, string message, string amount, CurrencyCode currency = CurrencyCode.VND, string service = "0208QRIBFTTA", string countryCode = "VN", string guid = "0010A000000727")
        {
           return QRCodeGenerator.CreateQRCode(bankAccount, bankId, message, amount, currency, service, countryCode,guid);           
        }
    }
}
