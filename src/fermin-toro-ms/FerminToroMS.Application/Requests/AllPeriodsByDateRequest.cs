using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AllPeriodsByDateRequest
    {
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
    }
}
