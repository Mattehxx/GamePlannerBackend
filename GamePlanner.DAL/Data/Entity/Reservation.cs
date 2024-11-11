using GamePlanner.DAL.Data.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner.DAL.Data.Db
{
    public class Reservation
    {
        public required int ReservationId { get; set; }
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
        public required bool IsDelete { get; set; } = false;
        public string? UserId { get; set; }
        public required int GameSessionId { get; set; }
        public ApplicationUser? User { get; set; }
        public GameSession? GameSession { get; set; }
    }
}