using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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