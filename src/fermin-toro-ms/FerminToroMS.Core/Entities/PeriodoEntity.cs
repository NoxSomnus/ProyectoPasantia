using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class PeriodoEntity : BaseEntity
    {
        public string NombrePeriodo { get; set; } = null!;
        public string MesInicio { get; set; } = null!;
        public string? MesFin { get; set; }
        public int Año { get; set; }
        public ICollection<Fechas_PagoEntity>? Fechas_Pago { get; set; }
        public ICollection<CronogramaEntity>? Cronogramas { get; set; }

    }
}
