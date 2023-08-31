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
    public class AllPeriodsByDateQuery : IRequest<List<PeriodResponse>>
    {
        public AllPeriodsByDateRequest _request;
        public AllPeriodsByDateQuery(AllPeriodsByDateRequest request) {  _request = request; }
    }
}
