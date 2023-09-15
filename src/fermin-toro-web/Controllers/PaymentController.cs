using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.CustomClasses;
using FerminToroWeb.Filters;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;

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

        [HttpGet]
        public async Task<IActionResult> CheckComprobantes(string ScheduleId)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/payment/checkcomprobantes?request=" + ScheduleId;
                var response = await _httpClient.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SomethingWentWrongView", "Messages");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<AllComprobantesByScheduleIdResponse>(responseContent);
                Response.Comprobantes = Response.Comprobantes.OrderBy(cadena => PaymentMapper.ObtenerValorMetodoPago(cadena)).ToList();
                var model = PaymentMapper.MapResponseToCheckComprobantesModel(Response);
                model.Response = Response;
                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
        

        public async Task<IActionResult> ChangePaymentStatus(List<string> PaymentId,
            List<string> Estado, List<double> Monto, List<string> NroTransaccion, List<string> NroCuenta,
            List<string> Correo, List<string> Telefono, List<string> FechaTransaccion,
            List<string> comprobanteIVA, List<double> TasaBCV) 
        {
            _verifySessionFilter.VerifySession(HttpContext);
            try
            {
                var apiUrl = apiurl.ApiUrl + "/payment/updatepaymentstate";
                var requestBody = new
                {
                    paymentsToUpdate = PaymentMapper.PaymentDataToUpdateMap(PaymentId,Estado,Monto,FechaTransaccion)
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
                
                return RedirectToAction("ScheduleUpdatedSucessfully", "Messages");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "No se pudo conectar con el servidor. Por favor, intenta nuevamente más tarde.");
            }
        }
    }
}
