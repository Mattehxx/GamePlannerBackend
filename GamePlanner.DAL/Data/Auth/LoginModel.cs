using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Auth
{
    public class LoginModel
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}