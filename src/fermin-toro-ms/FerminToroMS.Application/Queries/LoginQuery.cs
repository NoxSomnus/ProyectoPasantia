using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginRequest _request { get; set; }

        public LoginQuery(LoginRequest request)
        {
            _request = request;
        }

    }

}
