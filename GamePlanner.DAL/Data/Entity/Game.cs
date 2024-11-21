using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Entity
{
    public class Game
    {
        public required int GameId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDisabled { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public List<Session>? Sessions { get; set; }
        public List<Preference>? Preferences { get; set; }
    }
}