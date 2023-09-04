using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Filters;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

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
                var Response = JsonConvert.DeserializeObject<List<PaymentsDetailsResponse>>(responseContent);
                double total = 0;
                if (Response != null)
                {
                    foreach (var pago in Response)
                    {
                        total = total + pago.Mount;
                    }
                }
                var model = new PaymentDetailsModel 
                {
                    NombreEstudiante = Nombre +" "+ Apellido,
                    Pagos = Response,
                    Total = total,
                    InscriptionCode = Inscription
                };
                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}
