using FerminToroMS.Application.Responses;
using FerminToroWeb.CustomClasses;

namespace FerminToroWeb.Models
{
    public class PeriodSummaryModel
    {
        public string PeriodName { get; set; } = null!;
        public List<TotalOnModuls> TotalOnModuls { get; set; } = null!;
        public List<TotalOnModulsWithTurns> TotalOnModulsWithTurns { get; set; } = null!;
        public double TotalOnPeriod { get; set; }
        public double TotalOnline { get; set; }
        public double TotalPresencial { get; set; }
    }
}
