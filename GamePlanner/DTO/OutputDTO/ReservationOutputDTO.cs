using GamePlanner.DTO.InputDTO;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO
{
    public class ReservationOutputDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDelete { get; set; } = false;
        public string? UserId { get; set; }
        public int GameSessionId { get; set; }
        public bool IsQueued { get; set; }
        public GameSessionOutputDTO? GameSession { get; set; }
    }
}