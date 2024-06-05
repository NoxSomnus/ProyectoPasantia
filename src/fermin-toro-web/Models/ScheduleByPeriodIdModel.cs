using FerminToroMS.Application.Responses;

namespace FerminToroWeb.Models
{
    public class ScheduleByPeriodIdModel
    {
        public string PeriodoId { get; set; } = null!;
        public bool Editable { get; set; } = true;
        public List<ScheduleResponse> schedules { get; set; } = null!;
    }
}
