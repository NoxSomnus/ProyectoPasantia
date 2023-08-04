using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddCoursesByCSVRequest
    {
        public List<AddCourseRequest> Courses { get; set; } = null!;
    }
}
