using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class MoveFreezeInscriptionsRequest
    {
        public List<Guid> InscriptionsIds { get; set; } = null!;
        public List<Guid> FreezeInscriptionsIds { get; set; } = null!;
        public List<Guid?> SchedulesId { get; set; } = null!;
    }
}
