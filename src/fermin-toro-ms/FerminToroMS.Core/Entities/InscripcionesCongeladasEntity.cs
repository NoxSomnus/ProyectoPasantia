using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class InscripcionesCongeladasEntity : BaseEntity
    {
        public Guid InscripcionId { get; set; }
        public InscripcionEntity Inscripcion { get; set; } = null!;
        public bool PlanificacionCerrada { get; set; }
    }
}
