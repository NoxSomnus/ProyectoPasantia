using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class InscripcionEntity : BaseEntity
    {
        public string EstadoSolvencia { get; set; } = null!;
        public bool FueraVenezuela { get; set; }
        public string? EstadoVenezuela { get; set; }
        public string? NotaAcademica { get; set; }
        public Guid CronogramaId { get; set; }
        public CronogramaEntity Cronograma { get; set; } = null!;
        public ICollection<DeudasEntity>? Deudas { get; set; }
        public ICollection<PagoEntity>? Pagos { get; set; }
    }
}
