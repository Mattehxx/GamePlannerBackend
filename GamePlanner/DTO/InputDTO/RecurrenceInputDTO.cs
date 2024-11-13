using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class RecurrenceInputDTO
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        public int Day { get; set; }
        public List<EventInputDTO>? Events { get; set; }
    }
}
