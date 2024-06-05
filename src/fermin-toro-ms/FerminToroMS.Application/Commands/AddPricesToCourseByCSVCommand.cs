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
    public class AddPricesToCourseByCSVCommand : IRequest<bool>
    {
        public AddPricesToCourseByCSVRequest _request { get; set; }
        public AddPricesToCourseByCSVCommand(AddPricesToCourseByCSVRequest request)
        {
            _request = request;
        }
    }

}
