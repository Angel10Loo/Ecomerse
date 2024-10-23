using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class GenerateToken : IGenerateToken
    {
        private JwtOption JwtOption;
        private readonly UserManager<ApplicationUser> _userManager;

        public GenerateToken(IOptions<JwtOption> options, UserManager<ApplicationUser> userManager)
        {
            JwtOption = options.Value;
            _userManager = userManager;

        }
        public async Task<string> GenerateTokenAsync(LoginRequestDto loginRequestDto)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            var key = Encoding.ASCII.GetBytes(JwtOption.Secret);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Sid, user.Id),
            };

            var token = new JwtSecurityToken(
                issuer: JwtOption.Issuer,
                audience: JwtOption.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}
