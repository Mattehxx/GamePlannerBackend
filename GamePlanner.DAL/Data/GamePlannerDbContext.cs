using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.DAL.Data
{
    public class GamePlannerDbContext : IdentityDbContext<ApplicationUser>
    {
        public GamePlannerDbContext(DbContextOptions<GamePlannerDbContext> options)
      : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Game> Games { get; set; }

        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }

        public DbSet<Recurrence> Recurrences { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}