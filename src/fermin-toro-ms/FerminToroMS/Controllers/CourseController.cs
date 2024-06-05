using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Requests;
using FerminToroMS.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController<CourseController>
    {
        private readonly IMediator _mediator;
        public CourseController(ILogger<CourseController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que registra cursos y sus modulos mediante carga de archivo .csv.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar un nuevo curso con sus modulos.</param>
        /// <returns>Un objeto JSON con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias para registrar un cursos y sus modulos al sistema.
        /// El método devuelve un objeto JSON con la información del estado de la operacion.
        /// </remarks>
 
        [HttpPost("AddByCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Register([FromBody] AddCoursesByCSVRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra cursos mediante carga de archivo csv");
            try
            {
                var command = new AddCoursesByCSVCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar los cursos");
                return StatusCode(500, "Ocurrió un error al agregar los cursos. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }

        }

        /// <summary>
        /// Método que registra los precios de los cursos mediante carga de archivo .csv.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar los precios de los cursos.</param>
        /// <returns>Un objeto JSON con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias para registrar los precios de los cursos segun
        /// modalidad y turno en el sistema.
        /// El método devuelve un objeto JSON con la información del estado de la operacion.
        /// </remarks>

        [HttpPost("PricesByCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddPrices([FromBody] AddPricesToCourseByCSVRequest request)
        {
            _logger.LogInformation("Entrando al metodo que añade precios de los cursos por carga de archivo");
            try
            {
                var command = new AddPricesToCourseByCSVCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError(ex, "Error AddPricesToCourseCommand.HandleAsync. {Mensaje}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar los precios de los cursos");
                return StatusCode(500, "Ocurrió un error al agregar los precios de los cursos. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }

        }
    }
}
