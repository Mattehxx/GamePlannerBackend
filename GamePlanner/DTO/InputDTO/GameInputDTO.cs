using GamePlanner.DAL.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class GameInputDTO
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDisabled { get; set; }
        public required bool IsDeleted { get; set; } = false;
    }
}
