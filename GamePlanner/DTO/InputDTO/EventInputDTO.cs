using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class EventInputDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required IFormFile Image { get; set; }
        public required string AdminId { get; set; }
        public required bool IsPublic { get; set; }
    }
}