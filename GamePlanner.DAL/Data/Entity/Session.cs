using GamePlanner.DAL.Data.Auth;

namespace GamePlanner.DAL.Data.Entity
{
    public class Session
    {
        public required int SessionId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Seats { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public string? MasterId { get; set; }
        public required int EventId { get; set; }
        public required int GameId { get; set; }
        public ApplicationUser? Master { get; set; }
        public Event? Event { get; set; }
        public Game? Game { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}