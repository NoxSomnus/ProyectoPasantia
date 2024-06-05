using FerminToroMS.Application.Responses;

namespace FerminToroWeb.Models
{
    public class UpdateScheduleModel
    {
        public string PeriodoId { get; set; } = null!;
        public List<ScheduleResponse> schedules { get; set; } = null!;
        public List<AllInstructorsResponse>? instructors { get; set; }
    }
}
