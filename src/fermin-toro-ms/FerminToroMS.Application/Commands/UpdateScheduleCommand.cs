using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class UpdateScheduleCommand : IRequest<bool>
    {
        public UpdateScheduleRequest _request;
        public UpdateScheduleCommand(UpdateScheduleRequest request)
        {
            _request = request;
        }
    }
}
