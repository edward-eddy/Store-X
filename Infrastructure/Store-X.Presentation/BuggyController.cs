using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store_X.Shared.ErrorModels;
using System;
using System.Collections.Generic;

namespace Store_X.Presentation
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class BuggyController : ControllerBase
    public class BuggyController : _ControllerParent
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var id = 2;
            var responce = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"Couldn't Find Product With Id: {id}"
            };
            return NotFound(responce);
        }

        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception("Server Error 500 Message");
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            var responce = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = $"Invalid Login"
            };
            return BadRequest(responce);
        }

        [HttpGet("400/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            id = 2;
            var responce = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = $"Invalid Login for user {id}"
            };
            return BadRequest(responce);
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized();
        }
    }
}
