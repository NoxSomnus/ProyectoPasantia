using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Commands
{
    public class UpdatePaymentStateCommand : IRequest<bool>
    {
        public UpdatePaymentStateRequest _request;
        public UpdatePaymentStateCommand(UpdatePaymentStateRequest request)
        {
            _request = request;
        }
    }
}
