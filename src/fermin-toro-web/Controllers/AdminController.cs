using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("Admin/")]
        public IActionResult MenuAdministrador()
        {
            var model = new AdminMenuModel
            {
                HttpContext = HttpContext
            };
            return View(model);
        }
        public IActionResult UploadCSVMigrationOptions()
        {
            return View();
        }
        //------------------------------------------------------------------
        public IActionResult UploadCoursesCSVView()
        {
            return View();
        }
        //------------------------------------------------------------------
        public IActionResult UploadSchedulesCSVView()
        {
            return View();
        }
        
    }
}
