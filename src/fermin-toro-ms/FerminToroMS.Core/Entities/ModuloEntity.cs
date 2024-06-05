using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class ModuloEntity : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public string CodigoExamen { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Diminutivo { get; set; } = string.Empty;
        public Guid CursoId { get; set; }
        public CursoEntity Curso { get; set; } = null!;
        public ICollection<EstudianteEntity>? EstudiantesAprobados { get; set; }
        public ICollection<CronogramaEntity>? Cronogramas { get; set; }
        public ICollection<Precio_Mod_TurnoEntity>? Precios { get; set; }
    }
}
