using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class AddPeriodCommand : IRequest<bool>
    {
        public AddPeriodRequest _request;
        public AddPeriodCommand(AddPeriodRequest request) { _request = request; }
    }
}
