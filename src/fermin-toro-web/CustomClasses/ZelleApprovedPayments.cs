using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.CustomClasses
{
    public class ZelleApprovedPayments : ApprovedPayments
    {
        public string Correo { get; set; } = null!;
    }
}
