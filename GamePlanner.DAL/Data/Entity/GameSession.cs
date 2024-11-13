using GamePlanner.DAL.Data.Auth;

namespace GamePlanner.DAL.Data.Db
{
    public class GameSession
    {
        public required int GameSessionId { get; set; }
        public required DateTime GameSessionStartDate { get; set; }
        public required DateTime GameSessionEndDate { get; set; }
        public required bool IsDelete { get; set; } = false;
        public required int TableId { get; set; }
        public required int EventId { get; set; }
        public string? MasterId { get; set; }
        public Table? Table { get; set; }
        public Event? Event { get; set; }
        public ApplicationUser? Master { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}