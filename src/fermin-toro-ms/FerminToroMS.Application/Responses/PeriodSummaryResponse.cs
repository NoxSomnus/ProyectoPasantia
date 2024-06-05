using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class PeriodSummaryResponse
    {
        public string PeriodName { get; set; } = null!;
        public double TotalOnPeriod { get; set; }
        public List<TotalOnModulsWithTurns> TotalOnModulsWithTurns { get; set; } = null!;
        public double TotalPresencial { get; set; }
        public double TotalOnline { get; set; }
    }
}
