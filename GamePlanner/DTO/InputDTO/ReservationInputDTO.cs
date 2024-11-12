using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class ReservationInputDTO
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(50)]
        public required string Surname { get; set; }
        [MaxLength(100)]
        public required string Email { get; set; }
        [MaxLength(25)]
        public required string Phone { get; set; }
        public required DateTime BirthDate { get; set; }
        public required bool IsConfirmed { get; set; }
        public bool IsDelete { get; set; } = false;
        public string? UserId { get; set; }
        public required int GameSessionId { get; set; }
        public bool IsQueued { get; set; }
        public GameSessionInputDTO? GameSession { get; set; }
    }
}
