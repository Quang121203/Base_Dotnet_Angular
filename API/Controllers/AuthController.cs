using API.DataAccess;
using API.Models.Domains;
using API.Models.DTOS;
using API.Services.Implements;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;

        public AuthController( IAuthService authService, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            this.authService = authService;
            this.unitOfWork = unitOfWork;
            this.tokenService = tokenService;
        }
        [Authorize]

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("/information")]
        public IActionResult Infomation()
        {
            var email = User.FindFirstValue(ClaimTypes.Role);     
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            return Ok(email);

        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {

            var register = await authService.Register(model);
            if (register == null) return BadRequest();
            return Ok(register);

        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginVM model)
        {

            var login = await authService.Login(model);
            return Ok(login);

        }

        [HttpPost("/refesh")]
        public async Task<IActionResult> Refesh([FromBody] string refeshToken)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            string accessToken = authorizationHeader.Substring("Bearer ".Length);
            var token = await authService.Refesh(accessToken, refeshToken);
            return Ok(token);

        }

    }
}