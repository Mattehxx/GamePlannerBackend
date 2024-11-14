using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class SessionOutputDTO
    {
        public required int SessionId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Seats { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public string? MasterId { get; set; }
        public required int EventId { get; set; }
        public required int GameId { get; set; }
    }
}
