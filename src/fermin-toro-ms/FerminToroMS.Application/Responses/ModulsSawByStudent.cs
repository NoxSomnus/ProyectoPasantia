using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class ModulsSawByStudent
    {
        public string CourseName { get; set; } = null!;
        public string ModulName { get; set; } = null!;
        public string ModulCode { get; set; } = null!;
        public string InscriptionDate { get; set; } = null!;
        public string InscriptionStatus { get; set; } = null!;
        public int NroInscripcion { get; set; }
        public Guid InscriptionId { get; set; }
        public bool HasPayment { get; set; } = false;
        public double TotalPaid { get; set; }
    }
}
