using FerminToroMS.Core.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class Empleado_PermisoEntity : BaseEntity
    {
        public Guid EmpleadoEntityId { get; set; }
        public EmpleadoEntity Empleado { get; set; } = null!;
        public Guid PermisoEntityId { get; set; }
        public PermisosEntity Permiso { get; set; } = null!;
    }
}
