using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ResponseDto> AssignRoleAsync(RegisterRequestDto registerRequestDto);

    }
}
