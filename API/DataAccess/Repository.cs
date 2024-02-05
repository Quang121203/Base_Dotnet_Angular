using API.Models.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DatabaseContext databaseContext;

        public Repository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<int> CountAsync(int pageSize)
        {
            var page=  await databaseContext.Set<T>().CountAsync() *1.0 /pageSize;
            return (int)Math.Ceiling(page);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predecate, int pageSize)
        {
            var page = await databaseContext.Set<T>().CountAsync(predecate) * 1.0 / pageSize;
            return (int)Math.Ceiling(page);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var existing = await databaseContext.Set<T>().FindAsync(id);
            if (existing != null)
            {
                databaseContext!.Set<T>().Remove(existing);
                return true;
            }
            return false;
        }

        public async Task<List<T>> GetAsync()
        {
            return await databaseContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predecate)
        {
            return await databaseContext.Set<T>().Where(predecate).ToListAsync();
        }

        public async Task<List<T>> GetAsync(int indexPage, int pageSize)
        {
            return await databaseContext.Set<T>().Skip((indexPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predecate, int indexPage, int pageSize)
        {
            return await databaseContext.Set<T>().Where(predecate).Skip((indexPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetSingleAsync(object id)
        {
            return (await databaseContext.Set<T>().FindAsync(id))!;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predecate)
        {
            return (await databaseContext.Set<T>().FirstOrDefaultAsync(predecate))!;
        }

        public async Task InsertAsync(T entity)
        {
            await databaseContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            databaseContext.Set<T>().Update(entity);
        }

        public async Task<List<T>> Random(int count)
        {
            Random rand = new Random();
            int skipper = rand.Next(0, databaseContext.Set<T>().Count());

            return await databaseContext.Set<T>().Skip(skipper).Take(count).ToListAsync();
        }
    }
}
