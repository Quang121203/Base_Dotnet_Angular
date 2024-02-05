using API.Models.DAL;
using API.Models.Domains;

namespace API.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext databaseContext;
        public IRepository<List> ListRepository {  get; set; }
        public IRepository<Movie> MovieRepository { get; set; }
        public IRepository<Token> TokenRepository { get; set; }


        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            this.ListRepository= new Repository<List>(this.databaseContext);
            this.MovieRepository = new Repository<Movie>(this.databaseContext);
            this.TokenRepository = new Repository<Token>(this.databaseContext);
        }

        public async Task SaveChangesAsync()
        {
            await this.databaseContext.SaveChangesAsync();
        }
    }
}
