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
    public class CheckPermissionQuery : IRequest<GeneralResponse>
    {
        public Guid UserId { get; set; }
        public string Permission { get; set; }
        public CheckPermissionQuery(Guid userId, string permission)
        {
            UserId = userId;
            Permission = permission;
        }

    }

}
