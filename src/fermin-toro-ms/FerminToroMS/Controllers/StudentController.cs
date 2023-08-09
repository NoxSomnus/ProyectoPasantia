using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Requests;
using FerminToroMS.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController<StudentController>
    {
        private readonly IMediator _mediator;
        public StudentController(ILogger<StudentController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que registra estudiantes mediante carga de archivo .csv.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar estudiantes.</param>
        /// <returns>Un bool con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias para registrar estudiantes al sistema.
        /// El método devuelve un bool con la información del estado de la operacion.
        /// </remarks>

        [HttpPost("AddByCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddByCSV([FromBody] AddStudentsByCSVRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var command = new AddStudentsByCSVCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los estudiantes");
                return StatusCode(500, "Ocurrió un error al migrar los estudiantes. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }

        }

    }
}
