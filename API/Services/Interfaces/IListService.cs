using API.Models.Domains;

namespace API.Services.Interfaces
{
    public interface IListService
    {
        public Task<object?> DeleteList(int id);
        public Task<object> GetList(string genre, string type);
        public Task<object> CreateList(List model);

    }
}
