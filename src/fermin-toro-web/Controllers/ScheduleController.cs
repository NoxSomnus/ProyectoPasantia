using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
