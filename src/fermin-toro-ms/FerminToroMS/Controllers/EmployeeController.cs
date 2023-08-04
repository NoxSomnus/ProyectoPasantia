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
    public class EmployeeController : BaseController<EmployeeController>
    {
        private readonly IMediator _mediator;
        public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que registra empleados.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar un nuevo empleado.</param>
        /// <returns>Un objeto JSON con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias registrar un empleado al sistema.
        /// El método devuelve un objeto JSON con la información del estado de la operacion.
        /// </remarks>

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Register([FromBody] EmployeeSignUpRequest request)
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado para loguearlo");
            try
            {
                var command = new EmployeeSignUpCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (DataAlreadyExistException ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error: El empleado ya esta registrado", ex.Message);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar el empleado");
                return StatusCode(500, "Ocurrió un error al añadir al empleado. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que registra nuevos permisos para los empleados.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias 
        /// para registrar un nuevo permiso.</param>
        /// <returns>Un objeto JSON con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias registrar un nuevo permiso.
        /// El método devuelve un objeto JSON con la información del estado de la operacion.
        /// </remarks>

        [HttpPost("AddPermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddPermission([FromBody] AddPermissionRequest request)
        {
            _logger.LogInformation("Entrando al metodo que añade nuevos permisos para los usuarios");
            try
            {
                var command = new AddPermissionCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar el permiso");
                return StatusCode(500, "Ocurrió un error al agregar el permiso. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que verifica un permiso de un usuario al acceder a una opcion.
        /// </summary>
        /// <param name="UserId">Guid que corresponde el usuario que quiere acceder a una opcion</param>
        /// <param name="PermissionName">String del permiso que debe tener el usuario para acceder a la opcion</param>
        /// <returns>Un objeto JSON con la información de si la operacion fue exitosa.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Post con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias registrar un nuevo permiso.
        /// El método devuelve un objeto JSON con la información del estado de la operacion.
        /// </remarks>

        [HttpGet("CheckPermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CheckPermission([FromQuery] Guid UserId, [FromQuery] string PermissionName)
        {
            _logger.LogInformation("Entrando al metodo que verifica si el usuario tiene permiso para acceder a una opcion");
            try
            {
                var command = new CheckPermissionQuery(UserId, PermissionName);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al verificar los permisos");
                return NotFound(ex.Message);
            }
            catch (UserIdNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al verificar los permisos");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al verificar los permisos");
                return StatusCode(500, "Ocurrió un error al verificar los permisos. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
    }
}
