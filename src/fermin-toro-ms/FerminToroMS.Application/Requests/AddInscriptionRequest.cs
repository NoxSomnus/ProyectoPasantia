using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddInscriptionRequest
    {
        public string PeriodName { get; set; } = null!;
        public int PeriodYear { get; set; }
        public string CourseName { get; set; } = null!;
        public RegularidadEnum Regularidad { get; set; }
        public string ModulName { get; set; } = null!;
        public TurnosEnum Turno { get; set; }
        public ModalidadEnum Modalidad { get; set; }
        public string Cedula { get; set; } = null!;
        public string InscriptionDate { get; set; } = null!;
        public int NroInscription { get; set; }
        public bool PagoPorCuotas { get; set; }
    }
}
