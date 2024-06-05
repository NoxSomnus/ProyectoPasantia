using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class Modulos_AprobadoEntity : BaseEntity
    {
        public Guid EstudianteId { get; set; }
        public Guid ModuloId { get; set; }
        public EstudianteEntity Estudiante { get; set; } = null!;
        public ModuloEntity Modulo { get; set; } = null!;
    }
}
