using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddPricesToCourseByCSVRequest
    {
        public List<PricesRequest> Prices { get; set; } = null!;
    }
}
