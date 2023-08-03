using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FerminToroMS.Application.Exceptions;

namespace FerminToroMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController<LoginController>
    {
        private readonly IMediator _mediator;
        public LoginController(ILogger<LoginController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que verifica usuario y contraseña de un empleado para loguearse.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias para hacer el login.</param>
        /// <returns>Un objeto JSON con la información de si el login fue exitoso o no.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP GET con un objeto JSON en el cuerpo de la solicitud 
        /// que contiene las propiedades necesarias para loguearse al sistema.
        /// El método devuelve un objeto JSON con la información del estado del login.
        /// </remarks>

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado para loguearlo");
            try
            {
                var query = new LoginQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (InvalidPasswordException ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error: Contraseña invalida", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error: No se pudo encontrar el usuario", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al agregar el servicio");
                return StatusCode(500, "Ocurrió un error en el inicio de sesion. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

    }
}
