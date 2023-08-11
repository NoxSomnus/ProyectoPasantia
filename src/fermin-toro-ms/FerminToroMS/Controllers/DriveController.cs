﻿using FerminToroMS.Application.Exceptions;
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
    }
}