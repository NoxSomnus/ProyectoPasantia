using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.CustomClasses
{
    public class PeriodWithSchedulesCode
    {
        public Guid PeriodId { get; set; }
        public string PeriodNameWithYear { get; set; } = null!;
        public List<ScheduleCodes> ScheduleCodes { get; set; } = null!;
    }
}
