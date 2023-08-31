using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class AllPeriodsByYearQuery : IRequest<List<PeriodResponse>>
    {
        public int Año { get; set; }
        public AllPeriodsByYearQuery(int año) 
        {
            Año = año;
        }
    }
}
