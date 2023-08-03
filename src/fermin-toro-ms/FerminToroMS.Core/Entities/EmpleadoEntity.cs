using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class EmpleadoEntity : BaseEntity
    {
        [Column(Order = 2)]
        public BigInteger Cedula { get; set; }
        [Column(Order = 3)]
        public string Nombre { get; set; } = null!;
        [Column(Order = 4)]
        public string Apellido { get; set; } = null!;
        [Column(Order = 5)]
        public string Correo { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool esAdmin { get; set; }
        public bool esDirector { get; set; }
        public bool esInstructor { get; set; }
        public ICollection<PermisosEntity>? Permisos { get; set; }
        public ICollection<CronogramaEntity>? CronogramasAsignados { get; set; }
    }
}
