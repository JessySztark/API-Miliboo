```using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Miliboo.Models;
using Miliboo.Models.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Miliboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MilibooDBContext _dbContext;

        public LoginController(IConfiguration config, MilibooDBContext dBContext)
        {
            _config = config;
            _dbContext = dBContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            var user = await GetUser(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        private async Task<Account> GetUser(User user)
        {
            return await _dbContext.Account.FirstOrDefaultAsync(u => u.Mail == user.Mail && u.Password == user.Password);
        }

        private string GenerateJwtToken(Account userInfo)
        {
            var securityKey = new
           SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
             new Claim(JwtRegisteredClaimNames.Sub, userInfo.Mail),
             //new Claim("role",userInfo.UserRole),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };
            var token = new JwtSecurityToken(
             issuer: _config["Jwt:Issuer"],
             audience: _config["Jwt:Audience"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
        
```
