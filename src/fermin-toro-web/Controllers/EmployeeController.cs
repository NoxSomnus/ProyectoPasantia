using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
