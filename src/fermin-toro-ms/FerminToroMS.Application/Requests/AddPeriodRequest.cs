using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddPeriodRequest
    {
        public Guid newId { get; set; }
        public string PeriodName { get; set; } = null!;
        public int Year { get; set; }
        public string StartMonth { get; set; } = null!;
        public string EndMonth { get; set; } = null!;
    }
}
