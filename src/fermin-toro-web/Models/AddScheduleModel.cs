using FerminToroMS.Application.Responses;
using Microsoft.Win32;

namespace FerminToroWeb.Models
{
    public class AddScheduleModel
    {
        public string PeriodoId { get; set; } = null!;
        public bool FromCreatePeriod { get; set; }
        public List<AllInstructorsResponse>? instructores { get; set; }
    }
}
