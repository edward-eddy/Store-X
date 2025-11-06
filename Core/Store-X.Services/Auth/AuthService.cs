using Microsoft.AspNetCore.Identity;
using Store_X.Domain.Entities.Identity;
using Store_X.Domain.Exceptions.BadRequest;
using Store_X.Domain.Exceptions.NotFound;
using Store_X.Domain.Exceptions.Unauthorized;
using Store_X.Services_Abstractions.Auth;
using Store_X.Shared.Dtos.Auth;

namespace Store_X.Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager) : IAuthServices
    {
        public async Task<UserResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

            var flag = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UnauthorizedException();

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = ""
            };
        }


        public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                DisplayName = request.DisplayName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(e => e.Description).ToList());

            return new UserResponse()
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                Token = "TODO"
            };
        }
    }
}
