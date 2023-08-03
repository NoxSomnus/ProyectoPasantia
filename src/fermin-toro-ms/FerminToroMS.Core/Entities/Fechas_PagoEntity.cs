using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class Fechas_PagoEntity : BaseEntity
    {
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public DateOnly FechaFinCuota { get; set; }
        public Guid PeriodoId { get; set; }
        public PeriodoEntity Periodo { get; set; } = null!;
    }
}
