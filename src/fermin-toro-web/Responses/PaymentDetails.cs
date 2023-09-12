using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class PaymentDetails
    {
        public double ModulPrice { get; set; }
        public bool ByCuota { get; set; }
        public List<PaymentsDetailsResponse>? Payments { get; set; }
    }
}
