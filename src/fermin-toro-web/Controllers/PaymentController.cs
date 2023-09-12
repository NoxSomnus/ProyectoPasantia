using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.CustomClasses;
using FerminToroWeb.Filters;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;

namespace FerminToroWeb.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        private readonly VerifySessionFilter _verifySessionFilter;
        public PaymentController(ILogger<PaymentController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
            _verifySessionFilter = new VerifySessionFilter();
        }
        [HttpGet]
        public async Task<IActionResult> Details(string InscriptionId, string Nombre, string Apellido, string Inscription)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/payment/paymentdetails?request=" + InscriptionId;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<PaymentDetails>(responseContent);
                double total = 0;
                double debt = 0;
                if (Response != null && Response.Payments != null)
                {
                    foreach (var pago in Response.Payments)
                    {
                        total = total + pago.Mount;
                    }
                    debt = Response.ByCuota ? Response.ModulPrice * 2 - total : Response.ModulPrice - total;
                }

                var model = new PaymentDetailsModel 
                {
                    NombreEstudiante = Nombre +" "+ Apellido,
                    Pagos = Response.Payments,
                    Total = total,
                    InscriptionCode = Inscription,
                    Debt = debt
                };

                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PeriodSummary(string PeriodId)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/payment/periodsummary?request="+PeriodId;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<PeriodSummaryResponse>(responseContent);
                var model = SummaryDataMapper.SummaryMap(Response);
                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}
