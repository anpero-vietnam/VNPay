using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Model.Alepay
{
    public class TransactionDetailModel
    {
        public string code { get; set; }=string.Empty;
        public string status { get; set; } = string.Empty;
        
        public string message { get; set; } = string.Empty;
        public string transactionCode { get; set; } = string.Empty;
        public string orderCode { get; set; } = string.Empty;
        public string amount { get; set; } = string.Empty;
        public string currency { get; set; } = string.Empty;
        public string buyerEmail { get; set; } = string.Empty;
        public string buyerPhone { get; set; } = string.Empty;
        public string buyerName { get; set; } = string.Empty;        
        public string reason { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        //public string merchantFee { get; set; } = string.Empty;
        public string bankName { get; set; } = string.Empty;
        public double payerFee { get; set; } = 0;
        public int? month { get; set; } =0;
    }
}
