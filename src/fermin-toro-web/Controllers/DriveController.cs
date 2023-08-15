using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Filters;
using FerminToroWeb.GoogleDriveAPI;
using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FerminToroWeb.Controllers
{
    public class DriveController : Controller
    {
        private readonly VerifySessionFilter _verifySessionFilter;
        private readonly ApiUrlConfigClass apiurl;
        private HttpClient _httpClient;
        public DriveController(ILogger<DriveController> logger)
        {
            apiurl = new ApiUrlConfigClass();
            _httpClient = new HttpClient();
            _verifySessionFilter = new VerifySessionFilter();
        }

        [HttpGet]
        public ActionResult GetGoogleDriveFiles()
        {
            return View(GoogleDriveRepository.GetDriveFiles());
        }

        [HttpPost]
        public ActionResult DeleteFile(GoogleDriveFiles file)
        {
            GoogleDriveRepository.DeleteFile(file);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            var Id = GoogleDriveRepository.FileUpload(file);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public ActionResult FailedUploadFileView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCoursesCSVFile(IFormFile file)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            var Id = GoogleDriveRepository.FileUploadCSV(file);
            if (Id == null) 
            {
                return RedirectToAction("UploadFailedView", "Messages");
            }
            var apiUrl = apiurl.ApiUrl + "/drive/processcoursescsvfile";
            var requestBody = new { drivefileid = Id };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest) 
                {
                    return RedirectToAction("BadCSVFormat", "Messages"); 
                }

                return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            return RedirectToAction("SuccessfulMessageView", "Messages");
        }
        [HttpPost]
        public async Task<IActionResult> UploadSchedulesCSVFile(IFormFile file)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            var Id = GoogleDriveRepository.FileUploadCSV(file);
            if (Id == null)
            {
                return RedirectToAction("UploadFailedView", "Messages");
            }
            var apiUrl = apiurl.ApiUrl + "/drive/processschedulescsvfile";
            var requestBody = new { drivefileid = Id };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return RedirectToAction("BadCSVFormat", "Messages");
                }

                return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            return RedirectToAction("SuccessfulMessageView", "Messages");
        }

        [HttpPost]
        public async Task<IActionResult> UploadStudentsCSVFile(IFormFile file)
        {
            _verifySessionFilter.VerifySession(HttpContext);
            var Id = GoogleDriveRepository.FileUploadCSV(file);
            if (Id == null)
            {
                return RedirectToAction("UploadFailedView", "Messages");
            }
            var apiUrl = apiurl.ApiUrl + ""; //PILA AQUI
            var requestBody = new { drivefileid = Id };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return RedirectToAction("BadCSVFormat", "Messages");
                }

                return RedirectToAction("SomethingWentWrongView", "Messages");
            }
            return RedirectToAction("SuccessfulMessageView", "Messages");
        }
    }
}
