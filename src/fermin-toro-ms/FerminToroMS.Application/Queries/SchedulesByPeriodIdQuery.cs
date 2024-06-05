using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class SchedulesByPeriodIdQuery : IRequest<List<ScheduleResponse>>
    {
        public Guid PeriodId { get; set; }
        public SchedulesByPeriodIdQuery(Guid periodId)
        {
            PeriodId = periodId;
        }
    }
}
