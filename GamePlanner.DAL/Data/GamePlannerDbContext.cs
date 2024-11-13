using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.DAL.Data
{
    public class GamePlannerDbContext(DbContextOptions<GamePlannerDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}