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
    public class DriveController : BaseController<DriveController>
    {
        private readonly IMediator _mediator;

        public DriveController(ILogger<DriveController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("ProcessCoursesCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessCoursesCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo del firebase");
            try
            {
                var query = new ProcessCoursesCSVFileQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex) 
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the debt file.");
            }
        }

        [HttpPost("ProcessSchedulesCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessSchedulesCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo de google drive");
            try
            {
                var query = new ProcessScheduleCSVFileQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the debt file.");
            }
        }

        [HttpPost("ProcessStudentsCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessStudentsCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo de google drive");
            try
            {
                var query = new ProcessStudentsCSVFileQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the debt file.");
            }
        }

        [HttpPost("ProcessInscriptionsCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessInscriptionsCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo de google drive");
            try
            {
                var query = new ProcessInscriptionsCSVFileQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the inscription file.");
            }
        }

        [HttpPost("ProcessPricesCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessPricesCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo de google drive");
            try
            {
                var query = new ProcessPricesCSVFileQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the inscription file.");
            }
        }

        [HttpPost("ProcessPayments1stCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessPayments1stCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo de google drive");
            try
            {
                var query = new ProcessPayments1stCSVQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the inscription file.");
            }
        }

        [HttpPost("ProcessPayments2ndCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ProcessPayments2ndCSVFile([FromBody] ProcessCSVFileRequest request)
        {
            _logger.LogInformation("Entrando al metodo que procesa el archivo de google drive");
            try
            {
                var query = new ProcessPayments2ndCSVQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError("Ocurrio un error en la lectura del archivo. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the inscription file.");
            }
        }
    }
}
