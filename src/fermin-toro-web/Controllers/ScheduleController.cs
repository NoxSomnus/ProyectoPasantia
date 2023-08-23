﻿using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Filters;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace FerminToroWeb.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        private readonly VerifySessionFilter _verifySessionFilter;
        private MapperResponseToModels _mapper;
        public ScheduleController(ILogger<ScheduleController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
            _verifySessionFilter = new VerifySessionFilter();
            _mapper = new MapperResponseToModels();
        }

        public IActionResult AddPeriod()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            var model = new AddPeriodModel();
            return View(model);
        }
        public IActionResult AddSchedule(string periodoid)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            var model = new AddScheduleModel { PeriodoId = periodoid };
            return View("~/Views/Schedule/AddSchedule.cshtml",model);
        }

        [HttpPost]
        public void AddScheduleAction(string PeriodoId, List<string> programa, List<string> modulos,
            List<string> fechaInicio, List<string> fechaFin, List<string> regularidad,
            List<string> turno, List<string> horario, List<string> modalidad, List<string> duracion,
            List<string> vacantes)
        {
            _verifySessionFilter.VerifySession(HttpContext);
        }

        public IActionResult UpdateView(UpdatePeriodModel model)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            return View(model);
        }

        public async Task<IActionResult> UpdatePeriodAction(UpdatePeriodModel period)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/schedule/updateperiod";
                var requestBody = new
                {
                    periodId = period.Id,
                    periodName = period.NombrePeriodo,
                    year = period.Año,
                    startMonth = period.MesInicio,
                    endMonth = period.MesFin
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
                return RedirectToAction("PeriodUpdated", "Messages");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> DeletePeriodAction(string _password, string _userId, string _periodId)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/schedule/updateperiod";
                var requestBody = new
                {
                    periodId = _periodId,
                    userId = _userId,
                    password = _password
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }); // Serializa el body a formato JSON
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                return RedirectToAction("PeriodUpdated", "Messages");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> AddPeriodAction(AddPeriodModel period)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/schedule/addperiod";
                var requestBody = new
                {
                    newId = Guid.NewGuid().ToString(),
                    periodName = period.NombrePeriodo,
                    year = period.Año,
                    startMonth = period.MesInicio,
                    endMonth = period.MesFin
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }); // Serializa el body a formato JSON
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                return AddSchedule(requestBody.newId);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> AllPeriods()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/schedule/allperiods";
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PeriodResponse>>(responseContent);
                var model = _mapper.MapPeriodResponseToModel(Response);
                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> SearchByYear(int año)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/schedule/periodsbyyear?año="+año;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PeriodResponse>>(responseContent);
                var model = _mapper.MapPeriodResponseToModel(Response);
                return View("~/Views/Schedule/AllPeriods.cshtml", model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> SearchByDate(string fechainicio, string fechafin)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                fechainicio = fechainicio.Substring(0, 8) + "01";
                DateTime fecha = DateTime.ParseExact(fechainicio, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string fechaInicio = fecha.ToString("dd/MM/yyyy");
                fecha = DateTime.ParseExact(fechafin, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string fechaFin = fecha.ToString("dd/MM/yyyy");
                var apiUrl = apiurl.ApiUrl + "/schedule/periodsbydate";

                var requestBody = new { startDate = fechaInicio, endDate = fechaFin };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }); // Serializa el body a formato JSON
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                // Envía la solicitud POST con el body en formato JSON
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PeriodResponse>>(responseContent);
                var model = _mapper.MapPeriodResponseToModel(Response);
                return View("~/Views/Schedule/AllPeriods.cshtml", model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AllPeriods(string id)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/schedule/schedulebyperiodid?request="+id;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<ScheduleResponse>>(responseContent);
                return View("~/Views/Schedule/AllSchedulesByPeriodId.cshtml",Response);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}