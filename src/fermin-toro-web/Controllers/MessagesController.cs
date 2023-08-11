using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult UploadSuccessfulView()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            { Message = "Archivo Subido con Éxito", 
              NextStep = "Haga click para volver al inicio",
              ButtonContent = "Inicio"
            });
        }

        public IActionResult EmployeeAdded()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Empleado Añadido al sistema",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult EmployeeAlreadyExists()
        {
            return View("~/Views/Messages/FailedMessageView.cshtml", new ErrorMessageModel
            {
                Error = "El usuario que intento registrar ya fue registrado",
                Message = "El nombre de usuario o cedula ya fue registrado",
                ButtonContent = "Volver"
            });
        }

        public IActionResult SomethingWentWrongView()
        {
            return View();
        }

        public IActionResult BadCSVFormat() 
        {
            return View("~/Views/Messages/FailedMessageView.cshtml", new ErrorMessageModel
            { Error = "El archivo no tiene el formato correcto", 
              Message = "Reintente con un archivo que cumpla con el formato",
              ButtonContent = "Volver"
            });
        }

        public IActionResult UploadFailedView()
        {

            return View("~/Views/Messages/FailedMessageView.cshtml",new ErrorMessageModel
            { Error = "No se pudo subir el archivo", 
              Message = "Reintente nuevamente",
              ButtonContent = "Volver"
            });
        }
    }
}
