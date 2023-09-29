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
    public class AddPayments2ndCSVCommand : IRequest<AddPayments2ndCSVResponse>
    {
        public List<AddPayments2ndCSVRequest> payments;
        public AddPayments2ndCSVCommand(List<AddPayments2ndCSVRequest> request)
        {
            payments = request;
        }
    }
}
