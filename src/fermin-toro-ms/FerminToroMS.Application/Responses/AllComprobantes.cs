using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AllComprobantes
    {
        public Guid PaymentId { get; set; }
        public int NroInscripcion { get; set; }
        public string StudentName { get; set; } = null!;
        public string StudentEmail { get; set; } = null!;
        public string StudentPhone { get; set; } = null!;
        public string UrlComprobante { get; set; } = null!;
        public double Monto { get; set; }
        public bool Cuota { get; set; }
        public string MetodoPago { get; set; } = null!;
        public double Monto_A_Pagar { get; set; } = 0;
        public string EstadoActual { get; set; } = null!;
    }
}
