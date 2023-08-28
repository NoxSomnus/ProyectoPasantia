using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class ScheduleResponse
    {
        public Guid ScheduleId { get; set; }
        public Guid PeriodId { get; set; }
        public string PeriodName { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string ModulName { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Regularidad { get; set; } = null!;
        public int Duracion { get; set; }
        public string Horario { get; set; } = null!;
        public string Fecha_Inicio { get; set; } = null!;
        public string? Fecha_Fin { get; set; }
        public int Horas { get; set; }
        public string Modalidad { get; set; } = null!;
        public Guid? InstructorId { get; set; }
        public string? InstructorAsignado { get; set; }
        public int NroVacantes { get; set; }
    }
}
