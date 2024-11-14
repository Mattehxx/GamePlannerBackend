using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DTO.InputDTO
{
    public class ReservationInputDTO
    {
        public required int SessionId { get; set; }
        public required string UserId { get; set; }
    }
}
