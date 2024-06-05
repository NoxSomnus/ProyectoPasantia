using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Exceptions
{
    public class BadCSVRequest : Exception
    {
        public BadCSVRequest(string message) : base(message)
        {
        }
    }
}
