using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IGenerateToken
    {
        Task<string> GenerateTokenAsync(LoginRequestDto loginRequestDto);
    }
}
