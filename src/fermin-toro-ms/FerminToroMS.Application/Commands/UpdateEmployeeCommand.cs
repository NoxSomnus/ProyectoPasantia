using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public UpdateEmployeeRequest _request { get; set; }
        public UpdateEmployeeCommand(UpdateEmployeeRequest request)
        {
            _request = request;
        }
    }
}
