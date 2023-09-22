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

        /// <summary>
        /// Método que consulta los pagos de una inscripcion
        /// </summary>
        /// <param name="request">Guid que especifica el id de la inscripcion.</param>
        /// <returns>Un objeto JSON con la información de todos los pagos de esa inscripcion.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get con un Guid recibido de la url
        /// </remarks>

        [HttpGet("CheckComprobantes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CheckComprobantes([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al metodo que consulta los pagos con comprobantes de un cronograma");
            try
            {
                var query = new AllComprobantesByScheduleIdQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (IdNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos con comprobante");
                return NotFound(ex.Message);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos con comprobante");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos con comprobante");
                return StatusCode(500, "Ocurrió un error al consultar los pagos con comprobante. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que actualiza los estados de los pagos con comprobantes
        /// </summary>
        /// <param name="request">UpdatePaymentStateRequest que contiene la informacion de los pagos a actualizar.</param>
        /// <returns>Un bool que dice si la operacion fue exitosa o no.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Patch
        /// </remarks>

        [HttpPatch("UpdatePaymentState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePaymentState([FromBody] UpdatePaymentStateRequest request)
        {
            _logger.LogInformation("Entrando al metodo que consulta los pagos de una inscripcion");
            try
            {
                var command = new UpdatePaymentStateCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos de la inscripcion");
                return StatusCode(500, "Ocurrió un error al consultar los pagos de la inscripcion. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }

        /// <summary>
        /// Método que consulta los pagos aprobados
        /// </summary>
        /// <returns>Un objeto JSON con la información de todos los pagos aprobados.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP Get
        /// </remarks>

        [HttpGet("ApprovedPayments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ApprovedPayments()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los pagos aprobados");
            try
            {
                var query = new AllApprovedPaymentsQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar los pagos aprobados");
                return StatusCode(500, "Ocurrió un error al consultar los pagos aprobados. Por favor, inténtelo de nuevo más tarde o contacte al soporte técnico si el problema persiste.");
            }
        }
    }
}
