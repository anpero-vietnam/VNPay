using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Model
{
    public class PaymentConfig
    {
        public string ChecksumKey { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string MerchantId { get; set; } = string.Empty;
        public string MerchantPassword { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PaymentCode { get; set; } = string.Empty;
        public bool Isdefault { get; set; } = false;
        public int PaymentFee { get; set; } = 0;
        public string Currency { get; set; } = "VND";
    }
}
