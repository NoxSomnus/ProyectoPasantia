using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class CreateScheduleCommand : IRequest<bool>
    {
        public CreateScheduleRequest _request;
        public CreateScheduleCommand(CreateScheduleRequest request)
        {
            _request = request;
        }
    }
}
