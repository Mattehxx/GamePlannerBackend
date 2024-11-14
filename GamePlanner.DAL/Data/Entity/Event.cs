using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Entity
{
    public class Event
    {
        public int EventId { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required string AdminId { get; set; }
        public ApplicationUser? AdminUser { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}