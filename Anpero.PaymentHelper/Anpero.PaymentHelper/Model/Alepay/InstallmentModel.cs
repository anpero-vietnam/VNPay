namespace Anpero.PaymentHelper.Model.Alepay
{
    internal class InstallmentModel
    {
        public string code { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;

        public InstallmentDataModel[]? data { get; set; }
    }

    internal class PaymentMethod
    {
        public string paymentMethod { get; set; } = string.Empty;
        public Periods[] periods { get; set; } = new Periods[0];
    }

}
