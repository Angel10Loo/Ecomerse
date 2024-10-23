using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;
using System.Text.Json;

namespace Mango.Web.Services
{
    public class AuthService : IAuthService
    {
        private IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {

            _baseService = baseService;
        }

        public async Task<ResponseDto> AssignRoleAsync(RegisterRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registerRequestDto,
                Url = $"{SD.CouponAPIBase}api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {

            JsonSerializer
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = $"{SD.CouponAPIBase}api/auth/login"
            });
        }

        public async Task<ResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registerRequestDto,
                Url = $"{SD.CouponAPIBase}api/auth/register"
            });
        }
    }
}
