
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<object?> Register(RegisterVM model);
        public Task<object> Login(LoginVM model);
        public Task<object> Refesh(string accessToken,string refeshToken);
    }
}
