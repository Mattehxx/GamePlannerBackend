using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Db
{
    public class Table
    {
        public required int TableId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public required int Seat { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public List<GameSession>? GameSessions { get; set; }
    }
}