using GamePlanner.DAL.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace GamePlanner.DAL.Data.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required DateTime BirthDate { get; set; }
        public string? ImgUrl { get; set; }
        public required int Level { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<Event>? AdminEvents { get; set; }
        public List<Session>? Sessions { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public List<Preference>? Preferences { get; set; }
    }
}