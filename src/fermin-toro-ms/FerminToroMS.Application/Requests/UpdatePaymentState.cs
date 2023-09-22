using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class UpdatePaymentState
    {
        public Guid PaymentId { get; set; }
        public string State { get; set; } = null!;
        public double Monto { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public string? AccountNumber { get; set; }
        public string? Email { get; set; }
        public double? TasaBCV { get; set; }
        public string? TransactionDate { get; set; }
        public string? ComprobanteIVA { get; set; }
    }
}
