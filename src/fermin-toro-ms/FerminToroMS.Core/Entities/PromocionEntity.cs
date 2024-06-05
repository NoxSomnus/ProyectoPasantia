using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class PromocionEntity : BaseEntity
    {
        public string NombrePromocion { get; set; } = null!;
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public int Descuento { get; set; }
        public Guid? CronogramaId { get; set; }
        public CronogramaEntity? Cronograma { get; set; }
    }
}
