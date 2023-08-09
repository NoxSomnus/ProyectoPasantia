using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminHome()
        {
            return View();
        }
        public IActionResult DirectorHome()
        {
            return View();
        }
        public IActionResult UploadCSVMigrationOptions()
        {
            return View();
        }
    }
}
