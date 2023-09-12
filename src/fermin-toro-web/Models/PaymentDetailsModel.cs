using FerminToroMS.Application.Responses;

namespace FerminToroWeb.Models
{
    public class PaymentDetailsModel
    {
        public string NombreEstudiante { get; set; } = null!;
        public List<PaymentsDetailsResponse>? Pagos { get; set; }
        public double Total { get; set; }
        public string InscriptionCode { get; set; } = null!;
        public double Debt { get; set; }
    }
}
