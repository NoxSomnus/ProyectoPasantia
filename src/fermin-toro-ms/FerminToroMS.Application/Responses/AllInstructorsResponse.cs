using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AllInstructorsResponse 
    {
        public Guid InstructorId { get; set; }
        public string InstructorName { get; set; } = null!;
    }
}
