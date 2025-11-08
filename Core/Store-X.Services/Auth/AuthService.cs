using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store_X.Domain.Entities.Identity;
using Store_X.Domain.Exceptions.BadRequest;
using Store_X.Domain.Exceptions.NotFound;
using Store_X.Domain.Exceptions.Unauthorized;
using Store_X.Services_Abstractions.Auth;
using Store_X.Shared.Dtos.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager, IConfiguration _configuration) : IAuthServices
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
                Token = await GenerateTokenAsync(user)
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
                Token = await GenerateTokenAsync(user)
            };
        }

        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            // TOKEN:
            // 1. Header    (Type, Algo)
            // 2. Payload    (Claims)
            // 3. Signature    (Key)

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            ;

            //var jwtOptions = _configuration.GetSection("JwtOptions");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecurityKey"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtOptions:Issuer"],
                    audience: _configuration["JwtOptions:Audience"],
                    claims: authClaims,
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["JwtOptions:DurationDays"])),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
