using FerminToroMS.Core.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class AbonoEntity : BaseEntity
    {
        public Guid PagoId { get; set; }
        public PagoEntity Pago { get; set; } = null!;
        public Guid EstudianteId { get; set; }
        public EstudianteEntity Estudiante { get; set; } = null!;
    }
}
