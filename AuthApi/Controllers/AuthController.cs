using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthApi.Data;
using AuthApi.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AppDbContext context, IConfiguration configuration) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid Credentials");

            var jwtToken = GenerateJwtToken(user);

            return Ok(new
            {
                user.Id,
                user.Username,
                Token = jwtToken
            });
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var secretKey = configuration.GetSection("JwtSettings:SecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey.Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AuthApiChallege",
                audience: "AuthApiChallege",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds       
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
