using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class TotalOnModulsWithTurns
    {
        public Guid ModulId { get; set; }
        public string ModulName { get; set; } = null!;
        public string ModulCode { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Regularidad { get; set; } = null!;
        public string Modalidad { get; set; } = null!;
        public string ProgramName { get; set; } = null!;
        public int CuotaQuantity { get; set; } = 0;
        public int BillQuantity { get; set; } = 0;
        public int ReciboQuantity { get; set; } = 0;
        public double Total { get; set; }
    }
}
