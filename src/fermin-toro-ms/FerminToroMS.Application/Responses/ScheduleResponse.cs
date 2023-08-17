using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class ScheduleResponse
    {
        public Guid PeriodoId { get; set; }
        public string Periodo { get; set; } = null!;
        public string Curso { get; set; } = null!;
        public string Modulo { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Horario { get; set; } = null!;
        public DateOnly Fecha_Inicio { get; set; }
        public DateOnly? Fecha_Fin { get; set; }
        public int Horas { get; set; }
        public string Modalidad { get; set; } = null!;
        public string? InstructorAsignado { get; set; }
        public string? InstructorAsistente { get; set; }
    }
}
