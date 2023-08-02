using FerminToroMS.Core.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class PermisosEntity : BaseEntity
    {
        public string NombrePermiso { get; set; } = null!;
    }
}
