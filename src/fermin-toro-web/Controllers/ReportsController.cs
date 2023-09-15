using FerminToroMS.Application.Responses;
using FerminToroWeb.Mappers;
using FerminToroWeb.Models;
using FerminToroWeb.PdfGenerator;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;

namespace FerminToroWeb.Controllers
{
    public class ReportsController : Controller
    {
        private PaymentPdfGenerator PdfGenerator;
        private IWebHostEnvironment _webHostEnvironment;
        public ReportsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            PdfGenerator = new PaymentPdfGenerator(_webHostEnvironment);
        }

        public IActionResult PaymentWithComprobantesReport(string modelo)
        {
            CheckComprobantesModel model = JsonConvert.DeserializeObject<CheckComprobantesModel>(modelo);
            return PdfGenerator.GenerateComprobantesPdf(model.Response);
        }
    }
}
