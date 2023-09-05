using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class PaymentsDetailsResponse
    {
        public bool EnDivisa { get; set; }
        public bool EsJuridico { get; set; }
        public bool CompletePayment { get; set; }
        public double Mount { get; set; }
        public string PaymentDate { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public int? NroFactura { get; set; }
        public int? NroRecibo { get; set; }
        public string? Comprobante { get; set; }
        public Guid? EmpresaJuridica { get; set; }
        public string Comments { get; set; } = null!;
    }
}
