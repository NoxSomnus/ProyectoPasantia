using FerminToroMS.Core.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class DeudasEntity : BaseEntity
    {
        public float MontoDeuda { get; set; }
        public bool Aplica_Arancel { get; set; }
        public bool Deuda_Vencida { get; set; }
        public Guid EstudianteId { get; set; }
        public EstudianteEntity Estudiante { get; set; } = null!;
        public Guid InscripcionId { get; set; }
        public InscripcionEntity Inscripcion { get; set; } = null!;
    }
}
