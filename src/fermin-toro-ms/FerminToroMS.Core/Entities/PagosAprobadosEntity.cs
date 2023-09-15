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
        public int? NroTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string? Correo { get ; set; }
        public int NroCuenta { get; set; }
        public string Nombre_Empleado { get; set; } = null!;
        public DateTime FechaConciliacion { get; set; }
        [ForeignKey("PagoEntity")]
        public Guid PagoId { get; set; }
        public PagoEntity Pago { get; set; } = null!;
    }
}
