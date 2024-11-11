using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Db
{
    public class Event
    {
        public int EventId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime EventDate { get; set; }
        public required DateTime EventEndDate { get; set; }
        public required int Duration { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required string AdminId { get; set; }
        public required int GameId { get; set; }
        public required int RecurrenceId { get; set; }
        public ApplicationUser? User { get; set; }
        public Recurrence? Recurrence { get; set; }
        public Game? Game { get; set; }
        public List<GameSession>? GameSessions { get; set; }
    }
}