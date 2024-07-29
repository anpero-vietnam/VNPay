namespace Anpero.PaymentHelper.Model.Alepay
{
    internal class Periods
    {
        public int month { get; set; }
        public double minAmount { get; set; }
        public double amountFee { get; set; }
        public double amountFinal { get; set; }
        public double amountByMonth { get; set; }
        public double payerFlatFee { get; set; }
        public float payerInstallmentFlatFee { get; set; }
        public double payerInstallmentPercentFee { get; set; }
    }
}
