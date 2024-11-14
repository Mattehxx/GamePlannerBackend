using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DTO.InputDTO
{
    public class ReservationInputDTO
    {
        public required bool IsNotified { get; set; } = false;
        public required bool IsConfirmed { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required string UserId { get; set; }
        public required int GameSessionId { get; set; }
        //public required bool IsQueued { get; set; }
        public required int SessionId { get; set; }
    }
}
