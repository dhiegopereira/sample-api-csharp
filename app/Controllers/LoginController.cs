using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sample_api_csharp.DTOs;
using sample_api_csharp.Models;
using sample_api_csharp.Repositories;
using sample_api_csharp.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace sample_api_csharp.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly LoginRepository _loginRepository;
        private readonly IConfiguration _configuration;


        public LoginController(LoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] LoginDTO login)
        {
            try
            {
                // Verifica se o DTO é válido
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Customer customer = _loginRepository.Auth(login);

                if (customer == null)
                {
                    return NotFound();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtKey = _configuration.GetSection("JWT:Key").Value;

                var key = Encoding.ASCII.GetBytes(jwtKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, customer.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token); 

                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
