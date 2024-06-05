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

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Register([FromBody] StudentSignUpRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra estudiantes mediante carga de archivo csv");
            try
            {
                var command = new StudentSignUpCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (DataAlreadyExistException ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error: El estudiante ya esta registrado", ex.Message);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al registrar el estudiante");
                return StatusCode(500, "Ocurrió un error al registrar al estudiante. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }

        }
        /// <summary>
        /// Método que consulta todos los estudiantes del sistema.
        /// </summary>
        /// <returns>Un lista de todos los estudiantes del sistema.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get 
        /// El método devuelve una lista de todos los estudiantes del sistema.
        /// </remarks>
        [HttpGet("AllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AllStudents()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los estudiantes del sistema");
            try
            {
                var query = new AllStudentsQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los estudiantes");
                return StatusCode(500, "Ocurrió un error al consultar los estudiantes. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta todos los estudiantes juridicos del sistema.
        /// </summary>
        /// <returns>Un lista de todos los estudiantes juridicos del sistema.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get 
        /// El método devuelve una lista de todos los estudiantes juridicos del sistema.
        /// </remarks>
        [HttpGet("Juridicos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Juridicos()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los estudiantes del sistema");
            try
            {
                var query = new StudentsJuridicosQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los estudiantes");
                return StatusCode(500, "Ocurrió un error al consultar los estudiantes. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        [HttpPatch("SetJuridicos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SetJuridicos()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los estudiantes del sistema");
            try
            {
                var command = new SetJuridicoStudentsCommand();
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los estudiantes");
                return StatusCode(500, "Ocurrió un error al consultar los estudiantes. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta un estudiante por Id.
        /// </summary>
        /// <returns>Un objeto JSON con las propiedades del estudiante.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get 
        /// El método objeto JSON con las propiedades del estudiante.
        /// </remarks>
        [HttpGet("byId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> byId([FromQuery] Guid id)
        {
            _logger.LogInformation("Entrando al metodo que consulta un estudiante por Id");
            try
            {
                var query = new StudentByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (UserIdNotFoundException ex)
            {
                _logger.LogError(ex, "Error StudentByIdQueryHandler.HandleAsync.", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar al estudiante");
                return StatusCode(500, "Ocurrió un error al consultar al estudiante. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
    }
}
