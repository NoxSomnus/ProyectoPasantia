using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AllStudentsResponse
    {
        public Guid StudentId { get; set; }
        public string Cedula { get; set; } = null!;
        public string StudentName { get; set; } = null!;
        public string StudentLastName { get; set; } = null!;
        public string StudentEmail { get; set; } = null!;
        public string StudentCellPhone { get; set; } = null!;
        public string? StudentAge { get; set; } = null!;
    }
}
