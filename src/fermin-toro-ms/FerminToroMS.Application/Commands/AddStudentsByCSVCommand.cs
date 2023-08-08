using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class AddStudentsByCSVCommand : IRequest<bool>
    {
        public AddStudentsByCSVRequest _request { get; set; }
        public AddStudentsByCSVCommand(AddStudentsByCSVRequest request)
        {
            _request = request;
        }
    }
}
