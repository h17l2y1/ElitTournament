using ElitTournament.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElitTournament.DAL.Config
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<League> Leagues { get; set; }

    }
}
