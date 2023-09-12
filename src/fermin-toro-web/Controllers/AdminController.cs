using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using FerminToroWeb.Filters;

namespace FerminToroWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly VerifySessionFilter _verifySessionFilter = new VerifySessionFilter();
        public IActionResult Index()
        {
            return View();
        }
        [Route("Admin/")]
        public IActionResult MenuAdministrador()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            var model = new AdminMenuModel
            {
                HttpContext = HttpContext
            };
            return View(model);
        }
        [ResponseCache(Duration = 600)]
        public IActionResult AdminEmployee()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            
            return View();
        }
        [ResponseCache(Duration = 600)]
        public IActionResult AdminSchedule()
        {
            _verifySessionFilter.VerifySession(HttpContext);

            return View();
        }

        public IActionResult AdminStudent()
        {
            _verifySessionFilter.VerifySession(HttpContext);

            return View();
        }

        public IActionResult UploadCSVMigrationOptions()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View();
        }
        //------------------------------------------------------------------
        public IActionResult UploadCoursesCSVView()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View();
        }
        //------------------------------------------------------------------
        public IActionResult UploadSchedulesCSVView()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View();
        }

        //------------------------------------------------------------------
        public IActionResult UploadStudentsCSVView()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View();
        }
        public IActionResult UploadInscriptionsCSVView()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View();
        }
        public IActionResult UploadPricesCSVView()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View();
        }

    }
}
