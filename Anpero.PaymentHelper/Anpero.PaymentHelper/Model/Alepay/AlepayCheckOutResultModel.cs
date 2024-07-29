using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Model.Alepay
{
    internal class AlepayCheckOutResultModel
    {
        public string code { get; set; }=string.Empty;
        public string message { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
        public string checkoutUrl { get; set; } = string.Empty;
    }
}
