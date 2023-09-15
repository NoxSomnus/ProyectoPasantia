using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AllComprobantesByScheduleIdResponse
    {
        public string CourseCompleteName { get; set; } = null!;
        public Guid ModulId { get; set; }
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
        public List<AllComprobantes> Comprobantes { get; set; } = null!;
    }
}
