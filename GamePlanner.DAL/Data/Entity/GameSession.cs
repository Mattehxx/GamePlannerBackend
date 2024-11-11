using GamePlanner.DAL.Data.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner.DAL.Data.Db
{
    public class GameSession
    {
        public required int GameSessionId { get; set; }
        public required DateTime GameSessionDate { get; set; }
        public required DateTime GameSessionEndTime { get; set; }
        public required bool IsDelete { get; set; } = false;
        public required int TableId { get; set; }
        public required int EventId { get; set; }
        public required int MasterId { get; set; }
        public Table? Table { get; set; }
        public Event? Event { get; set; }
        public ApplicationUser? Master { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}