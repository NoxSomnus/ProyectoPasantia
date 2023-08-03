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
        public Guid EmpleadoId { get; set; }
        public EmpleadoEntity Empleado { get; set; } = null!;
        public Guid PermisoId { get; set; }
        public PermisosEntity Permiso { get; set; } = null!;
    }
}
