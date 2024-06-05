using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class EmployeeByIdQuery : IRequest<EmployeeResponse>
    {
        public Guid EmployeeId { get; set; }
        public EmployeeByIdQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
