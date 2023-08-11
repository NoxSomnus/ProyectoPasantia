using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FerminToroWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
        }

        [Route("Admin/Employees/Add")]
        public async Task<IActionResult> Add()
        {        
            var apiUrl = apiurl.ApiUrl + "/employee/allpermissions";
            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var Response = JsonConvert.DeserializeObject<List<PermissionResponse>>(responseContent);
            var model = new AddEmployeeModel();
            model.permisos = Response;
            return View("~/Views/Admin/AddNewEmployee.cshtml", model);
        }

        public async Task<IActionResult> AddAction(AddEmployeeModel _usuario)
        {
            var permissionsAssigned = new List<AssignPermissionRequest>();
            foreach (var permissions in _usuario.permisos) 
            {
                var permiso = new AssignPermissionRequest { PermisoId = permissions.IdPermiso };
                permissionsAssigned.Add(permiso);
            }
            var apiUrl = apiurl.ApiUrl + "/employee/register";
            var requestBody = new
            {
                cedula = _usuario.Cedula,
                nombre = _usuario.Name,
                apellido = _usuario.LastName,
                correo = _usuario.Email,
                username = _usuario.UserName,
                password = _usuario.Password,
                esAdmin = _usuario.esAdmin,
                esDirector = _usuario.esDirector,
                esInstructor = _usuario.esInstructor,
                permisos = permissionsAssigned

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
                    return RedirectToAction("EmployeeAlreadyExists", "Messages"); 
                }
                return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            return RedirectToAction("EmployeeAdded", "Messages");
        }
    }
}
