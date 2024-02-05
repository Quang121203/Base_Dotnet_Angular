using API.Models.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace API.Models.DAL
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<List>? Lists { get; set; }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
