using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Entity
{
    public class Reservation
    {
        public required int ReservationId { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public required bool IsConfirmed { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required int SessionId { get; set; }
        public required string UserId { get; set; }
        public Session? Session { get; set; }
        public ApplicationUser? User { get; set; }
    }
}