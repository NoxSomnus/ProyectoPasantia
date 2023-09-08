using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class FreezedInscriptionResponse
    {
        public Guid InscriptionId { get; set; }
        public Guid FreezeInscriptionId { get; set; }
        public Guid ScheduleId { get; set; }
        public string ScheduleCode { get; set; } = null!;
        public string StudentName { get; set; } = null!;
        public int NroInscripcion { get; set; }
        public bool PlanificacionCerrada { get; set; }
        public Guid ModulId { get; set; }
    }
}
