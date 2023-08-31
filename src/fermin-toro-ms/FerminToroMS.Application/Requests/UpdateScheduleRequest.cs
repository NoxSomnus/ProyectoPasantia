using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class UpdateScheduleRequest
    {
        public Guid PeriodId { get; set; }
        public List<ScheduleDataToUpdate> schedules { get; set; } = null!;
    }
}
