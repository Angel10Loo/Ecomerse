using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDto registerRequest);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
        Task<bool> AssignRole(string email, string role);

    }
}
