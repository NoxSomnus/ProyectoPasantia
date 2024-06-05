using FerminToroMS.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class ProcessCoursesCSVFileQuery : IRequest<bool>
    {
        public ProcessCSVFileRequest _request { get; set; }

        public ProcessCoursesCSVFileQuery(ProcessCSVFileRequest request)
        {
            _request = request;
        }
    }
}
