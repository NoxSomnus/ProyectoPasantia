﻿using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Filters;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;

namespace FerminToroWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        private readonly VerifySessionFilter _verifySessionFilter;
        private MapperResponseToModels _mapper;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
            _verifySessionFilter = new VerifySessionFilter();
            _mapper = new MapperResponseToModels();
        }

        [Route("Employees/Add")]
        public async Task<IActionResult> Add(AddEmployeeModel modelCreated)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/employee/allpermissions";
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PermissionResponse>>(responseContent);
                if (modelCreated.Cedula != "")
                {
                    modelCreated.permisos = Response;
                    modelCreated.Error = true;
                    return View("~/Views/Employee/AddNewEmployee.cshtml", modelCreated);
                }
                var model = new AddEmployeeModel();
                model.permisos = Response;
                return View("~/Views/Employee/AddNewEmployee.cshtml", model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> AddAction(AddEmployeeModel _usuario)
        {
            try
            {
                var permissionsAssigned = new List<AssignPermissionRequest>();
                foreach (var permissions in _usuario.permisos)
                {
                    if (permissions.Selected == true)
                    {
                        var permiso = new AssignPermissionRequest { PermisoId = permissions.IdPermiso };
                        permissionsAssigned.Add(permiso);
                    }
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
                        _usuario.Error = true;
                        return await Add(_usuario);
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

        public async Task<IActionResult> UpdateAction(UpdateEmployeeModel _usuario)
        {
            try
            {
                var permissionsAssigned = new List<AssignPermissionRequest>();
                if (_usuario.PermisosSeleccionados != null)
                {
                    foreach (var permissions in _usuario.PermisosSeleccionados)
                    {
                        var permiso = new AssignPermissionRequest { PermisoId = permissions };
                        permissionsAssigned.Add(permiso);
                    }
                }
                var apiUrl = apiurl.ApiUrl + "/employee/update";
                var requestBody = new
                {
                    id = _usuario.Id,
                    cedula = _usuario.Cedula,
                    nombre = _usuario.Nombre,
                    apellido = _usuario.Apellido,
                    correo = _usuario.Correo,
                    username = _usuario.Username,
                    esAdmin = _usuario.esAdmin,
                    esDirector = _usuario.esDirector,
                    esInstructor = _usuario.esInstructor,
                    permisos_Asignados = _usuario.permisos_asignados,
                    permisos_Nuevos = permissionsAssigned
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }); // Serializa el body a formato JSON
                var response = await _httpClient.PutAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                return RedirectToAction("EmployeeUpdated", "Messages");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/employee/byid?id="+id;
                var response = await _httpClient.DeleteAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("EmployeeNotFound", "Messages");
                    }
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                return RedirectToAction("EmployeeDeleted", "Messages");

            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> AllEmployees() 
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/employee/allemployees";
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllEmployeesResponse>>(responseContent);
                var model = _mapper.MapAllEmployeesResponseToModel(Response);
                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllEmployees(string id)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/employee/allpermissions";
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PermissionResponse>>(responseContent);
                //------------------------------------------------------------------------------------------
                var apiUrl2 = apiurl.ApiUrl + "/employee/byid?id=" + id;
                var response2 = await _httpClient.GetAsync(apiUrl2);
                if (!response2.IsSuccessStatusCode)
                {
                    if (response2.StatusCode == HttpStatusCode.NotFound) 
                    {
                        return RedirectToAction("EmployeeNotFound", "Messages");
                    }
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent2 = await response2.Content.ReadAsStringAsync();
                var Response2 = JsonConvert.DeserializeObject<EmployeeResponse>(responseContent2);
                var model = _mapper.MapResponseToUpdateEmployeeModel(Response2,Response);
                return View("~/Views/Employee/UpdateEmployee.cshtml",model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}
