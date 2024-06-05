using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class EmployeeSignUpCommand : IRequest<GeneralResponse>
    {
        public EmployeeSignUpRequest _request { get; set; }
        public EmployeeSignUpCommand (EmployeeSignUpRequest request) 
        {
            _request = request;
        }
    }
}
