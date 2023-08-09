using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult UploadCSVMigrationOptions()
        {
            return View();
        }

        public IActionResult UploadCoursesCSVView()
        {
            return View();
        }

        public IActionResult UploadCoursesCSVAction()
        {
            return RedirectToAction("UploadCoursesCSVFile", "Drive");
        }
    }
}
