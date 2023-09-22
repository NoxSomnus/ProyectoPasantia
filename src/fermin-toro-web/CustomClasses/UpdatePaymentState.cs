namespace FerminToroWeb.CustomClasses
{
    public class UpdatePaymentState
    {
        public string PaymentId { get; set; } = null!;
        public string State { get; set; } = null!;
        public double Monto { get; set; }
        public string? TransactionNumber { get; set; }
        public string? AccountNumber { get; set; }
        public string? Email { get; set; }
        public double? TasaBCV { get; set; }
        public string? TransactionDate { get; set; }
        public string? ComprobanteIVA { get; set; }
    }
}
