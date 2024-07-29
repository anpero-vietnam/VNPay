using Anpero.PaymentHelper.Model;
using Anpero.PaymentHelper.Model.Alepay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Control.Alepay
{   
    internal class AlepayCheckout
    {
        private readonly bool IsTest;
        private readonly PaymentConfig config;        
        public AlepayCheckout(bool isTest,PaymentConfig config)
        {
            IsTest = isTest;
            this.config = config;
        }

        public string? GetRedirectUrl(OrderModel basicModel)
        {
            basicModel.tokenKey = config.Token;
            AlepayUltil alepayUltil = new AlepayUltil();
            basicModel.signature = alepayUltil.GetSignature(basicModel, config.ChecksumKey);
            var postData = HttpHelper<AlepayCheckOutResultModel>.PostJson(IsTest ? "https://alepay-v3-sandbox.nganluong.vn/api/v3/checkout/request-payment" : "https://alepay-v3.nganluong.vn/api/v3/checkout/request-payment", basicModel);
            if (postData != null)
            {
                if (!string.IsNullOrEmpty(postData?.code) && Convert.ToInt32(postData.code) == 0)
                {
                    return postData.checkoutUrl;
                }
                else
                {
                    return postData?.message;
                }
            }
            return null;    
        }
        public TransactionDetailModel? GetTransactionDetail(string transactionCode, string tokenKey, string checksumKey)
        {
            AlepayUltil alepayUltil = new AlepayUltil();
            TransactionResultModel transaction = new TransactionResultModel();
            transaction.transactionCode = transactionCode;
            transaction.tokenKey = tokenKey;
            transaction.signature = alepayUltil.GetSignature(transaction, checksumKey);
            return HttpHelper<TransactionDetailModel>.PostJson(IsTest ? "https://alepay-v3-sandbox.nganluong.vn/api/v3/checkout/get-transaction-info" : "https://alepay-v3.nganluong.vn/api/v3/checkout/get-transaction-info", transaction);
        }
        //public InstallmentModel? GetInstallmentInfo(double amount, string tokenKey, string checksumKey)
        //{
        //    AlepayUltil alepayUltil = new AlepayUltil();
        //    InstallmentRequestModel model = new InstallmentRequestModel();
        //    model.amount = amount;
        //    model.tokenKey = tokenKey;
        //    model.currencyCode = "vnd";
        //    model.signature = alepayUltil.GetSignature(model, checksumKey);
        //    return HttpHelper<InstallmentModel>.PostJson(IsTest ? "https://alepay-v3-sandbox.nganluong.vn/api/v3/checkout/get-installment-info" : "https://alepay-v3.nganluong.vn/api/v3/checkout/get-installment-info", model);
        //}
    }
}
