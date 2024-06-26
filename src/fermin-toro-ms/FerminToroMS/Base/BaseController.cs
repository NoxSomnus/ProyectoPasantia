﻿using Microsoft.AspNetCore.Mvc;

namespace FerminToroMS.Base
{
    public class BaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> _logger;

        protected BaseController(ILogger<TController> logger)
        {
            _logger = logger;
        }

        [NonAction]
        protected ObjectResult Created(object response)
        {
            return StatusCode(201, response);
        }

        [NonAction]
        protected ObjectResult NonAuthoritativeInformation(object response)
        {
            return StatusCode(203, response);
        }

        [NonAction]
        protected new ObjectResult Unauthorized(object response)
        {
            return StatusCode(401, response);
        }

        [NonAction]
        protected ObjectResult Forbidden(object response)
        {
            return StatusCode(403, response);
        }

        [NonAction]
        protected ObjectResult MethodNotAllowed(object response)
        {
            return StatusCode(405, response);
        }

        [NonAction]
        protected ObjectResult NotAcceptable(object response)
        {
            return StatusCode(406, response);
        }

        [NonAction]
        protected new ObjectResult Conflict(object response)
        {
            return StatusCode(409, response);
        }

        [NonAction]
        protected ObjectResult PreconditionFailed(object response)
        {
            return StatusCode(412, response);
        }

        [NonAction]
        protected ObjectResult RequestEntityTooLarge(object response)
        {
            return StatusCode(413, response);
        }

        [NonAction]
        protected ObjectResult Locked(object response)
        {
            return StatusCode(423, response);
        }

        [NonAction]
        protected ObjectResult TooManyRequests(object response)
        {
            return StatusCode(429, response);
        }

        [NonAction]
        protected NoContentResult Response204()
        {
            return NoContent();
        }
    }
}
