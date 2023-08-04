using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddCourseRequest
    {
        public string CourseName { get; set; } = null!;
        public string ModulName { get; set; } = null!;
    }
}
