using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anpero.PaymentHelper.Model.Alepay
{
    public partial class OrderModel
    {
        public bool allowDomestic { get; set; } = true;
        public string customMerchantId { get; set; }=string.Empty;

        public string tokenKey { get; set; } = string.Empty;
        public string orderCode { get; set; } = string.Empty;
        public int amount { get; set; }
        public string currency { get; set; } = string.Empty;
        public string orderDescription { get; set; } = string.Empty;
        public int totalItem { get; set; }

        public string returnUrl { get; set; } = string.Empty;
        public string cancelUrl { get; set; } = string.Empty;
        public string buyerName { get; set; } = string.Empty;
        public string buyerEmail { get; set; } = string.Empty;
        public string buyerPhone { get; set; } = string.Empty;
        public string buyerAddress { get; set; } = string.Empty;
        public string buyerCity { get; set; } = string.Empty;
        public string buyerCountry { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;

        // ===== Alepay =====
        //  : Cho phép thanh toán ngay với thẻ
        //quốc tế và trả góp 
        //1: chỉ thanh toán ngay với thẻ quốc tế 
        //2: Chỉ thanh toán trả góp 
        //3: Thanh toán ngay với thẻ ATM, IB, QRCODE, VIETQR, BANK_TRANSFER_ONLINE, thẻ quốc tế và thanh toán trả góp nếu thiết lập allowDomestic = true
        //4: Thanh toán ngay với thẻ ATM, IB, QRCODE, VIETQR, BANK_TRANSFER_ONLINE, và thẻ quốc tế nếu thiết lập allowDomestic = true
        // ===== end Alepay =====
        public int checkoutType { get; set; }
    }
}
