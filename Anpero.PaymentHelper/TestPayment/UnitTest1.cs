using Anpero.PaymentHelper;
using Anpero.PaymentHelper.Control.QR;
using Anpero.PaymentHelper.Model;
using Anpero.PaymentHelper.Model.Alepay;
using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;

namespace TestPayment
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAlepay()
        {
            PaymentConfig config = new PaymentConfig
            {
                PaymentCode = PaymentCode.Alepay,
                Token = "nScDKVsTlaf8q2M7WkzS8c2CwqxE7q",
                ChecksumKey = "mGzAYcOtYlXJ7bcbmAkMgRZ5CsJvSN",
                MerchantPassword = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCFkLliQPNHekG8T7PEI1l8P5kALF4pQUaLDWoclybS4WD2htd0ieN1covXlf"
            };
            //0.Chỉ thanh toán ngay và trả góp với thẻ quốc tế
            //1.Chỉ thanh toán ngay với thẻ quốc tế
            //2.Chỉ thanh toán trả góp
            //3.Thanh toán ngay với thẻ quốc tế và nội địa(ATM, IB, QRCODE, VIETQR, /BANK_TRANSFER_ONLINE), thanh /toán trả góp thiết lập
            //allowDomestic =true
            //4.Thanh toán ngay với thẻ quốc tế và nội
            //địa(ATM, IB, QRCODE, VIETQR,
            //BANK_TRANSFER_ONLINE) thiết lập
            //allowDomestic = true

            OrderModel order = new OrderModel
            {
                tokenKey = config.Token,
                allowDomestic = true,
                amount = 10000000,
                buyerAddress = "Hoan Kiem",
                buyerCity = "Ha Noi",
                buyerCountry = "Viet Nam",
                buyerEmail = "thangtd.hn@gmail.com",
                customMerchantId = "OD01",
                orderCode = "OD01",
                orderDescription = "some text",
                returnUrl = "https://dynamic.anpero.com",
                checkoutType = 3,
                cancelUrl = @"https://dynamic.anpero.com",
                currency = "vnd",
                buyerName = "Trần Duy Thắng",
                buyerPhone = "0906006580",
                totalItem = 1
            };
            IAllPayment paymentHelper = new AllPayment(config, true);
            var postData = paymentHelper.GetCheckoutUrl(order);

            Assert.IsTrue(!string.IsNullOrEmpty(postData?.code) && Convert.ToInt32(postData.code) == 0);
        }
        [TestMethod]
        public void TestAlepayCallback()
        {

          
            PaymentConfig config = new PaymentConfig
            {
                PaymentCode = PaymentCode.Alepay,
                Token = "nScDKVsTlaf8q2M7WkzS8c2CwqxE7q",
                ChecksumKey = "mGzAYcOtYlXJ7bcbmAkMgRZ5CsJvSN",
                MerchantPassword = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCFkLliQPNHekG8T7PEI1l8P5kALF4pQUaLDWoclybS4WD2htd0ieN1covXlf"
            };
            IAllPayment paymentHelper = new AllPayment(config, true);


            var postData = paymentHelper.ProcessCallBackData("ALE00TFW3");
            Assert.IsTrue(!string.IsNullOrEmpty(postData?.code) && Convert.ToInt32(postData.code) == 0);
        }
        [TestMethod]
        public void TestQRcodeGeneral()
        {
            string bankAccount = "005704060117832";
            string bankId = "970441";
            string message = "oD 12134";
            string amount = "1000000";            
            var qrCode = QRCodeGenerator.CreateQRCode(bankAccount, bankId, message, amount);    
            Console.WriteLine(qrCode);
            Assert.IsTrue(qrCode!=null && qrCode.code=="0");
        }
       
       
    }
}