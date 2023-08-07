using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.IO;
using FerminToroWeb.GoogleDriveAPI;

namespace UCABPagaloTodoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiUrlConfig apiurl;
        private HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger)
        {
            apiurl = new ApiUrlConfig();
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginView()
        {

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAction(string username, string password)
        {
            var apiUrl = apiurl.ApiUrl + "/login/login";
            var requestBody = new { UserName = username, Password = password };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
                var esDirector = "No";
                if (loginResponse.IsDirector) 
                {
                    esDirector = "Si";
                    HttpContext.Session.SetString("EsDirector", esDirector);
                    return View("~/Views/Admin/DirectorHome.cshtml");
                }
                HttpContext.Session.SetString("EsDirector", esDirector);
                return View("~/Views/Admin/AdminHome.cshtml"); // si te da error de login action es este return, cambia la vista que retorna
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized) { return RedirectToAction("InvalidPasswordView", "Home"); }
            if (response.StatusCode == HttpStatusCode.NotFound) { return RedirectToAction("UserNotFoundView", "Home"); }
            return RedirectToAction("SomethingWentWrongView", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult InvalidPasswordView()
        {
            return View();
        }
        public IActionResult UserNotFoundView()
        {
            return View();
        }

        public IActionResult SomethingWentWrongView()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //---------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult GetGoogleDriveFiles()
        {
            return View(GoogleDriveRepository.GetDriveFiles());
        }

        [HttpPost]
        public ActionResult DeleteFile(GoogleDriveFiles file)
        {
            GoogleDriveRepository.DeleteFile(file);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            GoogleDriveRepository.FileUpload(file);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        
    }
}
    