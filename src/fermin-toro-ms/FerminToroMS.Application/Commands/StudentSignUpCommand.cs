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
    public class StudentSignUpCommand : IRequest<GeneralResponse>
    {
        public StudentSignUpRequest _request { get; set; }
        public StudentSignUpCommand(StudentSignUpRequest request)
        {
            _request = request;
        }

    }
}
