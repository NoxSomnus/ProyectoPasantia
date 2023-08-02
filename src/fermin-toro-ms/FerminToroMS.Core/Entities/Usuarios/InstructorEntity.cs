using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities.Usuarios
{
    public class InstructorEntity : EmpleadoEntity
    {
        public ICollection<PermisosEntity>? Permisos { get; set; }
        public ICollection<CronogramaEntity>? CronogramasAsignados { get; set; }
    }
}
