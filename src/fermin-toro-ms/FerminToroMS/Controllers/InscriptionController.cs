using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscriptionController : BaseController<InscriptionController>
    {
        private readonly IMediator _mediator;

        public InscriptionController(ILogger<InscriptionController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que registra inscripciones mediante carga de archivo .csv.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar las inscripciones.</param>
        /// <returns>Un objeto JSON con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud
        /// </remarks>

        [HttpPost("AddByCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddByCSV([FromBody] AddInscriptionByCSVRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra cursos mediante carga de archivo csv");
            try
            {
                var command = new AddInscriptionsByCSVCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (DataAlreadyExistException ex) 
            {
                _logger.LogError(ex, "Ocurrió un error al agregar las inscripciones");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar las inscripciones");
                return StatusCode(500, "Ocurrió un error al agregar las inscripciones. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta inscripciones de un cronograma
        /// </summary>
        /// <param name="request">Guid con el Id del cronograma.</param>
        /// <returns>Una lista de inscripciones del cronograma.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get con un parametro en la url
        /// </remarks>

        [HttpGet("InscriptionsByScheduleId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> InscriptionsByScheduleId([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al metodo que registra cursos mediante carga de archivo csv");
            try
            {
                var query = new AllInscriptionsByScheduleIdQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar las inscripciones");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar las inscripciones");
                return StatusCode(500, "Ocurrió un error al consultar las inscripciones. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta inscripciones congeladas
        /// </summary>
        /// <returns>Una lista de inscripciones congeladas.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get
        /// </remarks>

        [HttpGet("FreezedInscriptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> FreezedInscriptions()
        {
            _logger.LogInformation("Entrando al metodo que registra cursos mediante carga de archivo csv");
            try
            {
                var query = new AllFreezedInscriptionsQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar las inscripciones");
                return StatusCode(500, "Ocurrió un error al consultar las inscripciones. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
    }
}
