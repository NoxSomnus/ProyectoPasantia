using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class SchedulesEnabledResponse
    {
        public Guid ScheduleId { get; set; }
        public string ScheduleCode { get; set; } = null!;
        public Guid ModulId { get; set; }
    }
}
