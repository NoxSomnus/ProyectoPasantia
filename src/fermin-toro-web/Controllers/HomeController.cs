using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;

namespace UCABPagaloTodoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Login()
        {
            string sessionClosedMessage = HttpContext.Session.GetString("SessionClosedMessage");
            if (!string.IsNullOrEmpty(sessionClosedMessage))
            {
                ViewBag.Message = sessionClosedMessage;
                HttpContext.Session.Remove("SessionClosedMessage");
            }
            return View(new LoginViewModel());
        }

        public IActionResult LoginWithoutPermission()
        {
            string sessionClosedMessage = HttpContext.Session.GetString("SessionClosedMessage");
            if (!string.IsNullOrEmpty(sessionClosedMessage))
            {
                ViewBag.Message = sessionClosedMessage;
                HttpContext.Session.Remove("SessionClosedMessage");
            }
            return View("~/Views/Home/Login.cshtml",
                new LoginViewModel 
                {
                    Username = "",
                    Password = "",
                    Error = true
                });
        }


        [HttpPost]
        public async Task<IActionResult> LoginAction(string username, string password)
        {
            try
            {
                var apiUrl = apiurl.ApiUrl + "/login";
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

                    var esDirector = "No";
                    var esAdmin = "No";
                    if (loginResponse.IsDirector)
                    {
                        esDirector = "Si";
                        HttpContext.Session.SetString("EsDirector", esDirector);
                    }
                    if (loginResponse.IsAdmin)
                    {
                        esAdmin = "Si";
                        HttpContext.Session.SetString("EsAdmin", esAdmin);
                    }
                    HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
                    HttpContext.Session.SetString("Username", loginResponse.Username);
                    HttpContext.Session.SetString("EsDirector", esDirector);
                    HttpContext.Session.SetString("EsAdmin", esAdmin);
                    return RedirectToAction("VerifyLoginPermission", "Home"); 
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized) { return RedirectToAction("InvalidPasswordView", "Home"); }
                if (response.StatusCode == HttpStatusCode.NotFound) { return RedirectToAction("UserNotFoundView", "Home"); }
                return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            catch (HttpRequestException) 
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> VerifyLoginPermission()
        {
            var Admin = HttpContext.Session.GetString("EsDirector");
            var Director = HttpContext.Session.GetString("EsAdmin");
            if (Admin == "Si" || Director == "Si") 
            {
                return RedirectToAction("MenuAdministrador", "Admin");
            }
            var UserId = HttpContext.Session.GetString("UserId");
            var apiUrl = apiurl.ApiUrl + "/employee/checkpermission?userid=" + UserId + "&permissionname=" + "Iniciar Sesion";
            var response = await _httpClient.GetAsync(apiUrl);
            // Envía la solicitud POST con el body en formato JSON
            if (!response.IsSuccessStatusCode)
            {
                    HttpContext.Session.Clear();
                    return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var Response = JsonConvert.DeserializeObject<GeneralResponse>(responseContent);
            if (!Response.Success)
            {
                    HttpContext.Session.Clear();
                    return RedirectToAction("LoginWithoutPermission", "Home");
            }
            return RedirectToAction("MenuAdministrador", "Admin"); //CAMBIAR PARA UNA VISTA DE EMPLEADO NO ADMIN

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Login");
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
    }
}