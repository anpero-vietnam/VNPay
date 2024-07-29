using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Model.Alepay
{
    internal  class InstallmentRequestModel
    {
        public string tokenKey { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
        public double amount { get; set; } = 0;
        public string currencyCode { get; set; } = string.Empty;
    }
}
