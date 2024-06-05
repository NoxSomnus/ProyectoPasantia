using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class InscriptionsPaymentsResponse
    {
        public Guid InscripcionId { get; set; }
        public int NroInscripcion { get; set; }
        public string StudentName { get; set; } = null!;
        public string Fecha { get; set; } = null!;
        public double? Monto { get; set; }
        public int? NroFactura { get; set; }
        public int? NroRecibo { get; set; }
        public bool Cuotas { get; set; }
        public bool Divisa { get; set; }
        public string? UrlComprobante { get; set; }
        public string Estado { get; set; } = null!;
        public string? Comentario { get; set; }
        public Guid MetodoPagoId { get; set; }
        public string MetodoPago { get; set; } = null!;

    }
}
