using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Store_X.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("404")]
        public IActionResult GetNotFoundRequest()
        {
            var id = 2;
            return NotFound(new { message = $"Couldn't Find Product With Id: {id}", statusCode = 400, portal = 5000 });
        }

        [HttpGet("500")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception("Server Error 500");
        }

        [HttpGet("400")]
        public IActionResult GetBadRequest()
        {
            var id = 2;
            return BadRequest($"Invalid Login");
        }

        [HttpGet("400/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            id = 2;
            return BadRequest(new { message = $"Invalid Login for user {id}", statusCode = 400 });
        }

        [HttpGet("401")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized();
        }
    }
}
