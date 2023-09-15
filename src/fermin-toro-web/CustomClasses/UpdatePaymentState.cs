namespace FerminToroWeb.CustomClasses
{
    public class UpdatePaymentState
    {
        public string PaymentId { get; set; } = null!;
        public string State { get; set; } = null!;
        public double Monto { get; set; }
    }
}
