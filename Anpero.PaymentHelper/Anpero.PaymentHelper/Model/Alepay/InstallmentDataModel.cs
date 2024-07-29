namespace Anpero.PaymentHelper.Model.Alepay
{
    internal class InstallmentDataModel
    {
        public PaymentMethod[] paymentMethods { get; set; } = new PaymentMethod[0];
        public string bankCode { get; set; } = string.Empty;
        public string bankName { get; set; } = string.Empty;

    }
}
