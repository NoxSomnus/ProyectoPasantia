using FerminToroMS.Application.Responses;

namespace FerminToroWeb.Models
{
    public class CheckComprobantesModel
    {
        public string CourseCompleteName { get; set; } = null!;
        public string ModulCompleteName { get; set; } = null!;
        public Guid ModulId { get; set; }
        public string Code { get; set; } = null!;
        public string ModulName { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string Horario { get; set; } = null!;
        public string Modalidad { get; set; } = null!;
        public string Regularidad { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Instructor { get; set; } = null!;
        public List<AllComprobantes> Mercantil { get; set; } = null!;
        public List<AllComprobantes> BNC { get; set; } = null!;
        public List<AllComprobantes> Paypal { get; set; } = null!;
        public List<AllComprobantes> Zelle { get; set; } = null!;
        public AllComprobantesByScheduleIdResponse Response { get; set; } = null!;

    }
}
