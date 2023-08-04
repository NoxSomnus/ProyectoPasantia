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
                _logger.LogError(ex, "Ocurrió un error al agregar el servicio");
                return StatusCode(500, "Ocurrió un error en el inicio de sesion. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
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
                _logger.LogError(ex, "Ocurrió un error al agregar el servicio");
                return StatusCode(500, "Ocurrió un error en el inicio de sesion. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
    }
}
