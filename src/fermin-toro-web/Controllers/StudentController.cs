using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Filters;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using UCABPagaloTodoWeb.Controllers;

namespace FerminToroWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        private readonly VerifySessionFilter _verifySessionFilter;
        private MapperResponseToModels _mapper;
        public StudentController(ILogger<ScheduleController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
            _verifySessionFilter = new VerifySessionFilter();
            _mapper = new MapperResponseToModels();
        }
        
        public IActionResult SignUp()
        {
            return View(new StudentSignUpModel());
        }

        public IActionResult SignUpError(StudentSignUpModel _model)
        {
            _model.Error = true;
            return View("~/Views/Student/SignUp.cshtml",_model);
        }

        public async Task<IActionResult> SignUpAction(StudentSignUpModel _usuario)
        {
            try
            {
                var apiUrl = apiurl.ApiUrl + "/student/register";
                var requestBody = new
                {
                    cedula = _usuario.Cedula,
                    nombre = _usuario.Nombre,
                    apellido = _usuario.Apellido,
                    correo = _usuario.Correo,
                    direccion = _usuario.Direccion,
                    fecha_Nac = _usuario.Fecha_Nac,
                    rango_Edad = _usuario.Rango_Edad,
                    telefono = _usuario.Telefono,
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }); // Serializa el body a formato JSON
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        return SignUpError(_usuario);
                    }
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                return RedirectToAction("EmployeeAdded", "Messages");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> AllStudents()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/student/allstudents";
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllStudentsResponse>>(responseContent);                
                return View(Response);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> StudentDetail(string id)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/student/byid?id=" + id;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<StudentResponse>(responseContent);
                return View("~/Views/Student/StudentDetail.cshtml", Response);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}

