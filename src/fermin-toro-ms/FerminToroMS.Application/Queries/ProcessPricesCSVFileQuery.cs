using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class ProcessPricesCSVFileQuery : IRequest<bool>
    {
        public ProcessCSVFileRequest _request { get; set; }

        public ProcessPricesCSVFileQuery(ProcessCSVFileRequest request)
        {
            _request = request;
        }

    }
}
