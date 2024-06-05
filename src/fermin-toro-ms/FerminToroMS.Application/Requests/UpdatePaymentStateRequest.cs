using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class UpdatePaymentStateRequest
    {
        public List<UpdatePaymentState> PaymentsToUpdate { get; set; } = null!;
        public Guid Empleado { get; set; }
    }
}
