using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class PeriodSummaryQuery : IRequest<PeriodSummaryResponse>
    {
        public Guid PeriodId { get; set; }
        public PeriodSummaryQuery(Guid periodid) 
        {
            PeriodId = periodid;
        }
    }
}
