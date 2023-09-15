using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class Metodo_PagoEntity : BaseEntity
    {
        public string NombreMetodo { get; set; } = null!;
        public string? Rif { get; set; }
        public string? Cuenta { get; set; }
        public string? NombreDe { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool EnDivisa { get; set; }
        public ICollection<PagoEntity>? Pagos { get; set; }
    }
}
