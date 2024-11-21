using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class ApplicationUserOutputDTO : ApplicationUser
    {
        public string? Role { get; set; }
    }
}
