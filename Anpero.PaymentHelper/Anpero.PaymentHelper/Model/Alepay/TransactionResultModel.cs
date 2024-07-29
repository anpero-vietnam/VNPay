using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Model.Alepay
{
    internal  partial class TransactionResultModel
    {
        public string transactionCode { get; set; } = string.Empty;
        public string tokenKey { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
    }
}
