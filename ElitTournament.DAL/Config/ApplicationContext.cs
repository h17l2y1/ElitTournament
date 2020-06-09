using ElitTournament.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElitTournament.DAL.Config
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
