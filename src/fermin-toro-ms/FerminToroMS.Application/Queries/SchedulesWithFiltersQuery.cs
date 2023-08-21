using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class SchedulesWithFiltersQuery : IRequest<List<ScheduleResponse>>
    {
        public SchedulesWithFiltersRequest _request;
        public SchedulesWithFiltersQuery(SchedulesWithFiltersRequest request)
        {
            _request = request;
        }
    }
}
