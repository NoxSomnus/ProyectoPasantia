using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult LoginView()
        {

            return View(new LoginViewModel());
        }

        public IActionResult Login()
        {

            return View(new LoginViewModel());
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
                    if (loginResponse.IsDirector)
                    {
                        esDirector = "Si";
                        HttpContext.Session.SetString("EsDirector", esDirector);
                    }
                    HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
                    HttpContext.Session.SetString("Username", loginResponse.Username);
                    HttpContext.Session.SetString("EsDirector", esDirector);
                    return RedirectToAction("MenuAdministrador","Admin"); 
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