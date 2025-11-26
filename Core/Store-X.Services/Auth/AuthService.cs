using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store_X.Domain.Entities.Identity;
using Store_X.Domain.Exceptions.BadRequest;
using Store_X.Domain.Exceptions.NotFound;
using Store_X.Domain.Exceptions.Unauthorized;
using Store_X.Services_Abstractions.Auth;
using Store_X.Shared;
using Store_X.Shared.Dtos.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Auth
{
    public class AuthService(
        UserManager<AppUser> _userManager,
        /*IConfiguration _configuration*/ IOptions<JwtOptions> options,
        IMapper _mapper
        ) : IAuthServices
    {

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);
            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<AddressDto?> GetCurrentUserAddressAsync(string email)
        {
            //var user = await _userManager.FindByEmailAsync(email); // This Function Doesn't Load Navigational Property
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);
            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto request, string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);

            if (user.Address is null)
            {
                user.Address = _mapper.Map<Address>(request);
            }
            else
            {
                user.Address.FirstName = request.FirstName;
                user.Address.LastName = request.LastName;
                user.Address.City = request.City;
                user.Address.Street = request.Street;
                user.Address.Country = request.Country;
            }
            await _userManager.UpdateAsync(user);

            return _mapper.Map<AddressDto>(user.Address);
        }

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

            var jwtOptions = options.Value;


            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecurityKey"]));

            //var token = new JwtSecurityToken(
            //        issuer: _configuration["JwtOptions:Issuer"],
            //        audience: _configuration["JwtOptions:Audience"],
            //        claims: authClaims,
            //        expires: DateTime.Now.AddDays(double.Parse(_configuration["JwtOptions:DurationDays"])),
            //        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            //    );

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));

            var token = new JwtSecurityToken(
                    issuer: jwtOptions.Issuer,
                    audience: jwtOptions.Audience,
                    claims: authClaims,
                    expires: DateTime.Now.AddDays(jwtOptions.DurationDays),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
