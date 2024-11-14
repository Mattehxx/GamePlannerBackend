using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DTO.InputDTO
{
    public class SessionInputDTO
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Seats { get; set; }
        public string? MasterId { get; set; }
        public required int EventId { get; set; }
        public required int GameId { get; set; }
    }
}
