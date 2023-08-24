using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : BaseController<ScheduleController>
    {
        private readonly IMediator _mediator;
        public ScheduleController(ILogger<ScheduleController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que registra cronogramas mediante carga de archivo .csv.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar cronogramas.</param>
        /// <returns>Un bool con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias para registrar los cronogramas al sistema.
        /// El método devuelve un bool con la información del estado de la operacion.
        /// </remarks>

        [HttpPost("AddByCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddByCSV([FromBody] AddScheduleByCSVRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var command = new AddScheduleByCSVCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los cronogramas");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los cronogramas");
                return StatusCode(500, "Ocurrió un error al migrar los cronogramas. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que añade un nuevo periodo.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para añadir un periodo.</param>
        /// <returns>Un bool con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// </remarks>

        [HttpPost("AddPeriod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddPeriod([FromBody] AddPeriodRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var command = new AddPeriodCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar un nuevo periodo");
                return StatusCode(500, "Ocurrió un error al agregar el periodo. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }


        [HttpPost("AddSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddSchedule([FromBody] CreateScheduleRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra los registros de un cronograma");
            try
            {
                var command = new CreateScheduleCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar el cronograma");
                return NotFound(ex.Message);
            }
            catch (IdNotFoundException ex) 
            {
                _logger.LogError(ex, "Ocurrió un error al agregar el cronograma");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar un nuevo cronograma");
                return StatusCode(500, "Ocurrió un error al agregar el cronograma. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que actualiza un periodo.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para actualizar un periodo.</param>
        /// <returns>Un bool con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Patch con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias para actualizar un periodo.
        /// </remarks>

        [HttpPut("UpdatePeriod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePeriod([FromBody] UpdatePeriodRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var command = new UpdatePeriodCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (IdNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al actualizar el periodo");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los cronogramas");
                return StatusCode(500, "Ocurrió un error al actualizar el periodo. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta todos los periodos registrados.
        /// </summary>
        /// <returns>Una objeto JSON que tiene una lista de la informacion de los periodos.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get
        /// </remarks>

        [HttpGet("AllPeriods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AllPeriods()
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var query = new AllPeriodsQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los cronogramas");
                return StatusCode(500, "Ocurrió un error al migrar los cronogramas. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta todos los periodos registrados de un año.
        /// </summary>
        /// <returns>Una objeto JSON que tiene una lista de la informacion de los periodos.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get
        /// </remarks>

        [HttpGet("PeriodsbyYear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> byYear([FromQuery] int año)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var query = new AllPeriodsByYearQuery(año);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los cronogramas");
                return StatusCode(500, "Ocurrió un error al migrar los cronogramas. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta todos los periodos registrados segun un rango de fecha.
        /// </summary>
        /// <returns>Una objeto JSON que tiene una lista de la informacion de los periodos.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get
        /// </remarks>

        [HttpPost("PeriodsbyDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> byDate([FromBody] AllPeriodsByDateRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var query = new AllPeriodsByDateQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al migrar los cronogramas");
                return StatusCode(500, "Ocurrió un error al migrar los cronogramas. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        [HttpGet("ScheduleByPeriodId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ScheduleByPeriodId([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var query = new SchedulesByPeriodIdQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los cronogramas");
                return StatusCode(500, "Ocurrió un error al consultar los cronogramas. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
        [HttpPost("SchedulesWithFilters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ScheduleWithFilters([FromBody] SchedulesWithFiltersRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var query = new SchedulesWithFiltersQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los cronogramas");
                return StatusCode(500, "Ocurrió un error al consultar los cronogramas. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

    }
}
