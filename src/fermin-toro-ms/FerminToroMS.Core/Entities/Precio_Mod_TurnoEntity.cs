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
        public RegularidadEnum Regularidad { get; set; }
        public TurnosEnum Turno { get; set; }
        public double Precio { get; set; }
        public Guid ModuloId { get; set; }
        public ModuloEntity Modulo { get; set; } = null!;
        public bool PorCuotas { get; set; }
    }
}
