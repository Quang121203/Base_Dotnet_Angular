using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<object?> UpdateUser(UserVM model);
        public Task<object?> DeleteUser(string id);
        public Task<object?> GetAllUser();
        public Task<object?> GetUser(string id);
        public Task<object?> GetUserStats();

    }
}
