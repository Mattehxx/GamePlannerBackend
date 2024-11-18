using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Auth
{
    public class RegisterModel
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Password { get; set; }
    }
}