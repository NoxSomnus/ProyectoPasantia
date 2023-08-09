using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class AddScheduleByCSVCommand : IRequest<bool>
    {
        public AddScheduleByCSVRequest _request { get; set; }
        public AddScheduleByCSVCommand(AddScheduleByCSVRequest request)
        {
            _request = request;
        }
    }
}
