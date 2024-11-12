using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO
{
    public class EventOutputDTO
    {
        public int EventId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public string ImgUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string AdminId { get; set; }
        public int GameId { get; set; }
        public int RecurrenceId { get; set; }
        public ApplicationUserOutputDTO? User { get; set; }
        public RecurrenceOutputDTO? Recurrence { get; set; }
        public GameOutputDTO? Game { get; set; }
        public List<GameSessionOutputDTO>? GameSessions { get; set; }
    }
}