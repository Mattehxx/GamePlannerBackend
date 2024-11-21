using GamePlanner.DAL.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class GameInputDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required IFormFile ImgUrl { get; set; }
    }
}
