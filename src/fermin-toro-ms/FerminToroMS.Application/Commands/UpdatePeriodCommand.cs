using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class UpdatePeriodCommand : IRequest<bool>
    {
        public UpdatePeriodRequest _request;
        public UpdatePeriodCommand(UpdatePeriodRequest request) { _request = request; }
    }
}
