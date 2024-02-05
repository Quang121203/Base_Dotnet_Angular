using API.Models.Domains;

namespace API.DataAccess
{
    public interface IUnitOfWork
    {
        public IRepository<List> ListRepository { get; set; }
        public IRepository<Movie> MovieRepository { get; set; }
        public IRepository<Token> TokenRepository { get; set; }

        Task SaveChangesAsync();
    }
}
