using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class PagosAprobadosEntity : BaseEntity
    {
        public string NroTransaccion { get; set; } = null!;
        public DateTime FechaTransaccion { get; set; }
        public string? Correo { get ; set; }
        public string? NroCuentaPagoMovil { get; set; }
        public string? ComprobanteIVA { get; set; }
        public double? TasaBCV { get; set; }
        public string Nombre_Empleado { get; set; } = null!;
        public DateTime FechaConciliacion { get; set; }
        [ForeignKey("PagoEntity")]
        public Guid PagoId { get; set; }
        public PagoEntity Pago { get; set; } = null!;
    }
}
