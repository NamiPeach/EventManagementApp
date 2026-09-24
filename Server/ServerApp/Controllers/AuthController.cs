using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET /Auth/Login — показує форму
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST /Auth/Login — приймає дані форми, перевіряє, видає токен
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (login == AdminCredentials.Login && password == AdminCredentials.Password)
            {
                var token = GenerateJwtToken();

                Response.Cookies.Append("AdminToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                });

                return RedirectToAction("Index", "Registrations");
            }

            ViewBag.Error = "Невірний логін або пароль";
            return View();
        }

        private string GenerateJwtToken()
        {
            var jwtKey = _configuration["Jwt:Key"]!;
            var jwtIssuer = _configuration["Jwt:Issuer"]!;
            var jwtAudience = _configuration["Jwt:Audience"]!;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, AdminCredentials.Login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
