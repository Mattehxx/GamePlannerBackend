using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO
{
    public class GameOutputDTO
    {
        public int GameId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<EventOutputDTO>? Events { get; set; }
    }
}
