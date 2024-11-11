using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Auth
{
    public class LoginModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}