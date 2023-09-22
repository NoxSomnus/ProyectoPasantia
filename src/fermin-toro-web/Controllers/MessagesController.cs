using FerminToroWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroWeb.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult ScheduleUpdatedSucessfully()
        {
            var model = new ScheduleUpdatedSuccessfullyModel { SchedulesDisabled = false };
            return View("~/Views/Messages/ScheduleUpdatedSucessfully.cshtml", model);
        }
        public IActionResult UploadSuccesfulView()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {   Message = "Archivo Subido con Éxito", 
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult InscriptionsMovedSucessfully()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Acción completada con exito",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult ChangesSavedSucessfully()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Cambios guardados con éxito",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult UploadInscriptionsSuccesfulView()
        {
            return View("~/Views/Messages/UploadInscriptionsSuccesfulView.cshtml", new SuccesfulMessageModel
            {
                Message = "Archivo Subido con Éxito",
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

        public IActionResult StudentAdded()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Estudiante registrado en el sistema",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult PeriodAdded()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Periodo Creado",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult ScheduleAdded()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Cronograma Creado",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult EmployeeUpdated()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Información y permisos del empleado actualizados",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult PeriodUpdated()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Periodo Actualizado",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult EmployeeDeleted()
        {
            return View("~/Views/Messages/SuccessfulMessageView.cshtml", new SuccesfulMessageModel
            {
                Message = "Empleado eliminado del sistema",
                NextStep = "Haga click para volver al inicio",
                ButtonContent = "Inicio"
            });
        }

        public IActionResult EmployeeAlreadyExists()
        {
            return View("~/Views/Messages/FailedMessageView.cshtml", new ErrorMessageModel
            {
                Error = "El empleado que intento registrar ya fue registrado",
                Message = "El nombre de usuario o cedula ya fue registrado",
                ButtonContent = "Volver"
            });
        }

        public IActionResult EmployeeNotFound()
        {
            return View("~/Views/Messages/FailedMessageView.cshtml", new ErrorMessageModel
            {
                Error = "No se encontro el empleado",
                Message = "Vuelva a intentar nuevamente",
                ButtonContent = "Volver"
            });
        }

        public IActionResult SomethingWentWrongView()
        {
            return View();
        }

        public IActionResult FailedCSVRead()
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
