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
    public class AddPayments1stCSVCommand : IRequest<AddPayments1stCSVResponse>
    {
        public List<AddPayments1stCSVRequest> payments;
        public AddPayments1stCSVCommand(List<AddPayments1stCSVRequest> request)
        {
            payments = request;
        }
    }
}
