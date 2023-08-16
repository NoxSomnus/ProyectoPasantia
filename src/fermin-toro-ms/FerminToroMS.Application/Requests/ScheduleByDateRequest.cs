using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class ScheduleByDateRequest 
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
