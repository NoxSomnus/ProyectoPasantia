using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.CustomClasses;
using FerminToroWeb.Filters;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace FerminToroWeb.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        private readonly VerifySessionFilter _verifySessionFilter;
        private MapperResponseToModels _mapper;
        public InscriptionController(ILogger<InscriptionController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
            _verifySessionFilter = new VerifySessionFilter();
            _mapper = new MapperResponseToModels();
        }
        public async Task<IActionResult> InscriptionByScheduleId(string scheduleId)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/inscription/inscriptionsbyscheduleid?request=" + scheduleId;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<AllInscriptionsResponse>(responseContent);                
                return View("~/Views/Inscription/InscriptionByScheduleId.cshtml", Response);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> FreezedInscriptions()
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/inscription/freezedinscriptions";
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<FreezedInscriptionResponse>>(responseContent);
                var cerradas = Response.Where(c => c != null && c.PlanificacionCerrada == true);
                apiUrl = apiurl.ApiUrl + "/schedule/schedulesenabled";
                response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                responseContent = await response.Content.ReadAsStringAsync();
                var Response2 = JsonConvert.DeserializeObject<List<SchedulesEnabledResponse>>(responseContent);
                var model = new ClosedScheduleInscriptionsModel 
                { 
                    Cerradas = cerradas,
                    CronogramasDisponibles = Response2
                };
                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        public async Task<IActionResult> MoveFromClosedScheduleInscriptions(
            List<string> InscriptionId, List<string> FreezeInscriptionId, List<string> ScheduleId)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/inscription/movefreezeinscriptions";
                var requestBody = new
                {
                    inscriptionsIds = InscriptionId,
                    freezeInscriptionsIds = FreezeInscriptionId,
                    schedulesId = ScheduleId
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }); // Serializa el body a formato JSON
                var response = await _httpClient.PatchAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                return RedirectToAction("InscriptionsMovedSucessfully", "Messages");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}
