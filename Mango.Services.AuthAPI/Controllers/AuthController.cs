using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _responseDto;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                bool isSuccess = await _authService.AssignRole(registerRequestDto.Email, registerRequestDto.RoleName.ToUpper());

                if (!isSuccess)
                {
                    _responseDto.IsSucces = false;
                    _responseDto.ErrorMessage = "Error occur";
                    return BadRequest(_responseDto);
                }
                return Ok(isSuccess);
            }
            catch (Exception ex)
            {
                _responseDto.ErrorMessage += ex.Message;
                _responseDto.IsSucces = false;
                return BadRequest(_responseDto);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
        {
            try
            {
                if (user == null)
                    return BadRequest("Usuario no es valido");

                string response = await _authService.RegisterAsync(user);
                if (response != null)
                {
                    _responseDto.IsSucces = true;
                    _responseDto.Result = response;
                    return Ok(_responseDto);
                }
            }
            catch (Exception ex)
            {
                _responseDto.ErrorMessage = ex.Message;
                _responseDto.IsSucces = false;

                return BadRequest(_responseDto);
            }
            return BadRequest(_responseDto.IsSucces = false);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                LoginResponseDto dto = await _authService.LoginAsync(loginRequestDto);

                _responseDto.IsSucces = true;
                _responseDto.Result = dto;

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _responseDto.ErrorMessage += ex.Message;
                _responseDto.IsSucces = false;
                return BadRequest(_responseDto);
            }
        }
    }
}
