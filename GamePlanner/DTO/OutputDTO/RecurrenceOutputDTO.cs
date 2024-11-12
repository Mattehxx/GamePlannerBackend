using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO
{
    public class RecurrenceOutputDTO
    {
        public required int RecurrenceId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
    }
}