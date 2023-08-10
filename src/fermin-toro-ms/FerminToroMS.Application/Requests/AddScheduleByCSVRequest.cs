using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddScheduleByCSVRequest
    {
        public List<AddScheduleRequest> Schedules { get; set; } = null!;
    }
}
