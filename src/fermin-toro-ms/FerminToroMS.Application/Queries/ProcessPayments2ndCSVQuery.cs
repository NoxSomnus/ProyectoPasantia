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
    public class ProcessPayments2ndCSVQuery : IRequest<AddPayments2ndCSVResponse>
    {
        public ProcessCSVFileRequest _request { get; set; }

        public ProcessPayments2ndCSVQuery(ProcessCSVFileRequest request)
        {
            _request = request;
        }
    }
}
