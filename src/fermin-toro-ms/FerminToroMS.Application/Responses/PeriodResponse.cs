using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class PeriodResponse
    {
        public Guid PeriodId { get; set; }
        public string PeriodName { get; set; } = null!;
        public int Año { get; set; }
        public string MesInicio { get; set; } = null!;
        public string MesFin { get; set; } = null!;

    }
}
