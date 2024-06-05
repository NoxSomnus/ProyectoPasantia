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
    public class AddPermissionCommand : IRequest<GeneralResponse>
    {
        public AddPermissionRequest _request { get; set; }
        public AddPermissionCommand(AddPermissionRequest request)
        {
            _request = request;
        }
    }


}
