using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FerminToroMS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController<PaymentController>
    {
        private readonly IMediator _mediator;

        public PaymentController(ILogger<PaymentController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Método que consulta los pagos de una inscripcion
        /// </summary>
        /// <param name="request">Guid que especifica el id de la inscripcion.</param>
        /// <returns>Un objeto JSON con la información de todos los pagos de esa inscripcion.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get con un Guid recibido de la url
        /// </remarks>

        [HttpGet("PaymentDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PaymentDetails([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al metodo que consulta los pagos de una inscripcion");
            try
            {
                var query = new PaymentDetailsQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (IdNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos de la inscripcion");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos de la inscripcion");
                return StatusCode(500, "Ocurrió un error al consultar los pagos de la inscripcion. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta los pagos de una inscripcion
        /// </summary>
        /// <param name="request">Guid que especifica el id de la inscripcion.</param>
        /// <returns>Un objeto JSON con la información de todos los pagos de esa inscripcion.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get con un Guid recibido de la url
        /// </remarks>

        [HttpGet("PeriodSummary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PeriodSummary([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al metodo que consulta el summary del periodo");
            try
            {
                var query = new PeriodSummaryQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultarel summary del periodo");
                return StatusCode(500, "Ocurrió un error al consultar el summary del periodo. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
    }
}
