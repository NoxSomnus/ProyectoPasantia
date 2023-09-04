using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class TotalOnModuls
    {
        public string ModulName { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Regularidad { get; set; } = null!;
        public string Modalidad { get; set; } = null!;
        public string ProgramName { get; set; } = null!;
        public double Total { get; set; }
    }
}
