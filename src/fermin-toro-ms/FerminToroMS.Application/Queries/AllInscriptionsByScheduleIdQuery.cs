using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class AllInscriptionsByScheduleIdQuery : IRequest<AllInscriptionsResponse>
    {
        public Guid ScheduleId { get; set; }
        public AllInscriptionsByScheduleIdQuery(Guid scheduleId)
        {
            ScheduleId = scheduleId;
        }
    }
}
