using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class PaisesEntity
    {
        public int Id { get; set; }
        public string Iso { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
