using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store_X.Services_Abstractions;
using Store_X.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        // Check Email Exists
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var result = await _serviceManager.AuthServices.CheckEmailExistsAsync(email);
            return Ok(result);
        }

        // Get Current User
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthServices.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }

        // Get Current User Address
        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthServices.GetCurrentUserAddressAsync(email.Value);
            return Ok(result);
        }

        // Update Current User Address
        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthServices.UpdateCurrentUserAddressAsync(request, email.Value);
            return Ok(result);
        }
    }
}
