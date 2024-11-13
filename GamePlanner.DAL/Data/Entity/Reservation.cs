using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Db
{
    public class Reservation
    {
        public required int ReservationId { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Surname { get; set; }

        [RegularExpression(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,100}$", ErrorMessage = "Email format not valid")]
        public required string Email { get; set; }

        [RegularExpression(@"^\+?\d{1,3}?[- .]?\(?\d{1,4}?\)?[- .]?\d{1,4}[- .]?\d{1,9}$", ErrorMessage = "Phone number format not valid")]
        public required string Phone { get; set; }
        public required DateTime BirthDate { get; set; }
        public required bool IsConfirmed { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public string? UserId { get; set; }
        public required int GameSessionId { get; set; }
        public bool IsQueued { get; set; }
        public ApplicationUser? User { get; set; }
        public GameSession? GameSession { get; set; }
    }
}