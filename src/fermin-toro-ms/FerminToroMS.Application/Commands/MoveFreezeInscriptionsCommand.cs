using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class MoveFreezeInscriptionsCommand : IRequest<bool>
    {
        public MoveFreezeInscriptionsRequest _request { get; set; }
        public MoveFreezeInscriptionsCommand(MoveFreezeInscriptionsRequest request)
        {
            _request = request;
        }
    }
}
