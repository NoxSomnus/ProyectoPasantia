using FerminToroMS.Application.Responses;

namespace FerminToroWeb.Models
{
    public class ClosedScheduleInscriptionsModel
    {
        public IEnumerable<FreezedInscriptionResponse>? Cerradas { get; set; }
        public List<SchedulesEnabledResponse>? CronogramasDisponibles { get; set; }
    }
}
