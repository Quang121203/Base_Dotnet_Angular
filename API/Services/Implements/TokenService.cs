using API.DataAccess;
using API.Models.Domains;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Services.Implements
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> CreateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddSeconds(20),
                claims: claims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public async Task<Token> CreateToken(User user)
        {
            string accessToken = await CreateAccessToken(user);
            string refeshToken = CreateRefreshToken();

            Token token = new Token
            {
                RefeshToken = refeshToken,
                ExpiresRefeshToken = DateTime.Now.AddMinutes(20),
                AccessToken = accessToken,
                UserId =user.Id
            };
            
            await unitOfWork.TokenRepository.InsertAsync(token);
            await unitOfWork.SaveChangesAsync();


            return token;
        }
    }
}
