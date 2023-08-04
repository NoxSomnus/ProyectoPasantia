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
        public ICollection<PagoEntity>? Pagos { get; set; }
    }
}
