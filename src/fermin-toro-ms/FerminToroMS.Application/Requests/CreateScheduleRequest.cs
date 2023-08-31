using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class CreateScheduleRequest
    {
        public Guid PeriodId { get; set; }
        public List<ScheduleDataToCreate> Schedules { get; set; } = null!;
    }
}
