using Microsoft.AspNetCore.Mvc;
using Store_X.Services_Abstractions;
using Store_X.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Presentation
{
    public class AuthController(IServiceManager _serviceManager) : _ControllerParent
    {
        //Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _serviceManager.AuthServices.LoginAsync(request);
            return Ok(result);
        }

        //Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.AuthServices.RegisterAsync(request);
            return Ok(result);
        }
    }
}
