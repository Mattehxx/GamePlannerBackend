using GamePlanner.DAL.Data.Entity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Surname { get; set; }
        public required DateTime BirthDate { get; set; }
        public string? ImgUrl { get; set; }
        public required int Level { get; set; }
        public required bool IsDisabled { get; set; } = false;
        public required bool IsDeleted { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<Event>? AdminEvents { get; set; }
        public List<Session>? Sessions { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public List<Preference>? Preferences { get; set; }
    }
}