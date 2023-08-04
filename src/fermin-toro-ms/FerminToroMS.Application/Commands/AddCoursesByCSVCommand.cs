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
    public class AddCoursesByCSVCommand : IRequest<bool>
    {
        public AddCoursesByCSVRequest _request { get; set; }
        public AddCoursesByCSVCommand(AddCoursesByCSVRequest request)
        {
            _request = request;
        }

    }
}
