using FerminToroMS.Application.Responses;

namespace FerminToroWeb.Models
{
    public class ScheduleByPeriodIdModel
    {
        public string PeriodoId { get; set; } = null!;
        public List<ScheduleResponse> schedules { get; set; } = null!;
    }
}
