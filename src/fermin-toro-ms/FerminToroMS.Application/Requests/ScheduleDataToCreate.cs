using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class ScheduleDataToCreate
    {
        public string Programa { get; set; } = null!;
        public string Modulo { get; set; } = null!;
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        public RegularidadEnum Regularidad { get; set; } 
        public TurnosEnum Turno { get; set; }
        public string Horario { get; set; } = null!;
        public ModalidadEnum Modalidad { get; set; }
        public int Duracion { get; set; }
        public int Vacantes { get; set; }
        public string InstructorId { get; set; } = null!;
    }
}
