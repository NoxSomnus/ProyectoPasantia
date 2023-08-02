using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities.Usuarios
{
    public class PersonaEntity : BaseEntity
    {
        [Column(Order = 2)]
        public BigInteger Cedula { get; set; }
        [Column(Order = 3)]
        public string Nombre { get; set; } = null!;
        [Column(Order = 4)]
        public string Apellido { get; set; } = null!;
        [Column(Order = 5)]
        public string Correo { get; set; } = null!;
    }
}
