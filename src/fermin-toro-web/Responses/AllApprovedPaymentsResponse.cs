using FerminToroMS.Application.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AllApprovedPaymentsResponse
    {
        public List<MercantilApprovedPayments> PagosMercantil { get; set; } = null!;
        public List<BNCApprovedPayments> PagosBNC { get; set; } = null!;
        public List<PaypalApprovedPayments> PagosPaypal { get; set; } = null!;
        public List<ZelleApprovedPayments> PagosZelle { get; set; } = null!;
        public List<PeriodResponse> Periodos { get; set; } = null!;
    }
}
