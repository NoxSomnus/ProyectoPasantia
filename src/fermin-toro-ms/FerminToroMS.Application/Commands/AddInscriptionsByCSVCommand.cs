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
    public class AddInscriptionsByCSVCommand : IRequest<AddInscriptionsResponse>
    {
        public AddInscriptionByCSVRequest _request;
        public AddInscriptionsByCSVCommand(AddInscriptionByCSVRequest request)
        {
            _request = request;
        }
    }
}
