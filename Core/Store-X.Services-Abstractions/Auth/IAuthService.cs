using Store_X.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services_Abstractions.Auth
{
    public interface IAuthServices
    {
        Task<UserResponse?> LoginAsync(LoginRequest request);
        Task<UserResponse?> RegisterAsync(RegisterRequest request);

        // Check Email Exists
        Task<bool> CheckEmailExistsAsync(string email);
        // Get Current User
        Task<UserResponse?> GetCurrentUserAsync(string email);
        // Get Current User Address
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);
        // Update Current User Address
        Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto request, string email);
    }
}
