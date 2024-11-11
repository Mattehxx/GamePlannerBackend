using GamePlanner.DAL.Data.Db;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO
{
    public class TableOutputDTO
    {
        public int TableId { get; set; }
        public string Name { get; set; }
        public int Seat { get; set; }
        public bool IsDeleted { get; set; } = false;
        //public List<GameSessionOutputDTO>? GameSessions { get; set; }   ??
    }
}