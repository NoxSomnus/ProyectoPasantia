using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class RepresentanteEntity : BaseEntity
    {
        [Column(Order = 2)]
        public string Nombre { get; set; } = null!;
        [Column(Order = 3)]
        public string Apellido { get; set; } = null!;
        [Column(Order = 4)]
        public string? Correo { get; set; }
        [Column(Order = 5)]
        public string Telefono { get; set; } = null!;
    }
}
