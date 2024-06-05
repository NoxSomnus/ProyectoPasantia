using FerminToroMS.Application.CustomClasses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class GetAllSchedulesCodesByPeriodIdQuery : IRequest<List<ScheduleCodes>>
    {
        public Guid PeriodId { get; set; }
        public GetAllSchedulesCodesByPeriodIdQuery(Guid periodId)
        {
            PeriodId = periodId;
        }
    }
}
