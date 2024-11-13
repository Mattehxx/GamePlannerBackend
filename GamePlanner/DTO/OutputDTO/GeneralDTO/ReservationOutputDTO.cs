using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class ReservationOutputDTO
    {
        public required int ReservationId { get; set; }
        public required bool IsConfirmed { get; set; }
        public required bool IsDelete { get; set; } = false;
        public required bool IsQueued { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public string? UserId { get; set; }
        public required int GameSessionId { get; set; }
        public UserOutputDTO? User { get; set; }
        public GameSessionOutputDTO? GameSession { get; set; }
    }
}