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
    public class ProcessPayments1stCSVQuery : IRequest<AddPayments1stCSVResponse>
    {
        public ProcessCSVFileRequest _request { get; set; }

        public ProcessPayments1stCSVQuery(ProcessCSVFileRequest request)
        {
            _request = request;
        }
    }
}
