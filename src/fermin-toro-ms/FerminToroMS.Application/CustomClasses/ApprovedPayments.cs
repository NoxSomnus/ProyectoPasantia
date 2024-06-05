using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.CustomClasses
{
    public abstract class ApprovedPayments
    {
        public Guid PagoId { get; set; }
        public Guid PagoAprobadoId { get; set; }
        public string FechaTransaccion { get; set; } = null!;
        public string NroTransaccion { get; set; } = null!;
        public string FechaConciliacion { get; set; } = null!;
        public string Empleado_Conciliador { get; set; } = null!;
        public double Monto { get; set; }
        public bool Divisa { get; set; }
        public string CodigoCronograma { get; set; } = null!;
    }
}
