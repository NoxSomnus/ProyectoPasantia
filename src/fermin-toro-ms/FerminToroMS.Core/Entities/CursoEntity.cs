using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class CursoEntity : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public ICollection<ModuloEntity>? Modulos { get; set; }
        public string NombreCompleto { get; set; } = null!;
    }
}
