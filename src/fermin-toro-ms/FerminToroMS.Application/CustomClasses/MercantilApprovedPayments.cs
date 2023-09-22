using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.CustomClasses
{
    public class MercantilApprovedPayments : ApprovedPayments
    {
        public string? NroCuentaPagoMovil { get; set; }
        public double TasaBCV { get; set; }
        public string? ComprobanteIVA { get; set; }
    }
}
