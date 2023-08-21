using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class SchedulesWithFiltersRequest
    {
        public Guid PeriodId { get; set; }
        public List<int>? ByModalidad { get; set; }
        public List<int>? ByTurno { get; set; }
        public List<int>? ByRegularidad { get; set; }
        public List<string>? ByModulo { get; set; }
        public List<string>? ByCurso { get; set; }
    }
}
