using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddScheduleRequest
    {
        public string NombrePerido { get; set; } = null!;
        public int Año { get; set; }
        public string Meses { get; set; } = null!;
        public string Curso { get; set; } = null!;
        public RegularidadEnum Regularidad { get; set; }
        public string Modulo { get; set; } = null!;
        public TurnosEnum Turno { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string Horario { get; set; } = null!;
        public ModalidadEnum Modalidad { get; set; }
        public int Semanas { get; set; }

    }
}
