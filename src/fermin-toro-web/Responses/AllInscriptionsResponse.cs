using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AllInscriptionsResponse
    {
        public Guid ScheduleId { get; set; }
        public string CourseCompleteName { get; set; } = null!;
        public string ModulCompleteName { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string ModulName { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string Horario { get; set; } = null!;
        public string Modalidad { get; set; } = null!;
        public string Regularidad { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Instructor { get; set; } = null!;
        public List<StudentRegiteredOnInscriptionResponse> Students { get; set; } = null!;
        public List<InscriptionsPaymentsResponse> Payments { get; set; } = null!;
    }
}
