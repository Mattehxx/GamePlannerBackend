using GamePlanner.DAL.Data.Db;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string BirthDate { get; set; }
        public string? ImgUrl { get; set; }
        public required bool CanBeMaster { get; set; }
        public required int KnewledgeId { get; set; }
        public Knowledge? Knewledge { get; set; }
        public List<GameSession>? MasterGameSessions { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public List<Event>? AdminEvents { get; set; }
    }
}