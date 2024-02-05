using API.Models.Domains;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateAccessToken(User user);
        public string CreateRefreshToken();

        public Task<Token> CreateToken(User user);
    }
}
