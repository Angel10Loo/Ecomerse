using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGenerateToken _generateToken;
        public AuthService(RoleManager<IdentityRole> roleManager, AppDBContext db, UserManager<ApplicationUser> userManager, IGenerateToken generateToken)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            _generateToken = generateToken;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                bool roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                var result = await _userManager.AddToRoleAsync(user, role);

                if (result.Succeeded)
                    return true;

            }
            return false;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {

            string token = await _generateToken.GenerateTokenAsync(loginRequest);

            if (token == null)
                throw new InvalidOperationException();

            ApplicationUser user = await _userManager.FindByEmailAsync(loginRequest.UserName);

            UserDto userDto = new()
            {
                ID = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return new LoginResponseDto { Token = token, User = userDto };

        }

        public async Task<string> RegisterAsync(RegisterRequestDto registerRequest)
        {
            try
            {
                IdentityResult result = await _userManager.CreateAsync(
                 new ApplicationUser
                 {
                     Email = registerRequest.Email,
                     UserName = registerRequest.Name,
                     Name = registerRequest.Name,
                     PhoneNumber = registerRequest.PhoneNumber,
                 }, password: registerRequest.Password);

                if (result.Succeeded)
                    return "Usuario Registrado*";

            }
            catch (Exception ex)
            {

            }
            return null;

        }
    }
}
