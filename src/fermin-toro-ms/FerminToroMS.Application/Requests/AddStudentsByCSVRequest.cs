using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddStudentsByCSVRequest
    {
        public List<AddStudentRequest> Students { get; set; } = null!;
    }
}
