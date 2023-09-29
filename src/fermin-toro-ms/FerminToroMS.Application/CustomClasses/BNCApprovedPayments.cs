using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.CustomClasses
{
    public class BNCApprovedPayments : ApprovedPayments
    {
        public string NroCuenta { get; set; } = null!;
        public double TasaBCV { get; set; }
        public string? ComprobanteIVA { get; set; }
    }
}
