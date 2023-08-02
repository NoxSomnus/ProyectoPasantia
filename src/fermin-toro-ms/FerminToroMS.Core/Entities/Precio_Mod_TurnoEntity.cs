using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class Precio_Mod_TurnoEntity : BaseEntity
    {
        public ModalidadEnum Modalidad { get; set; }
        public TurnosEnum Turno { get; set; }
        public float Precio { get; set; }
        public Guid CursoId { get; set; }
    }
}
