using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class EventInputDTO
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime EventDate { get; set; }
        public required int Duration { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required string AdminId { get; set; }
        public required int GameId { get; set; }
        public required int RecurrenceId { get; set; }
        public RecurrenceInputDTO? Recurrence { get; set; }
        public GameInputDTO? Game { get; set; }
        public List<GameSessionInputDTO>? GameSessions { get; set; }
    }
}