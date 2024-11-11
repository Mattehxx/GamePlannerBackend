using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;

namespace GamePlanner.DTO.OutputDTO
{
    public class GameSessionOutputDTO
    {
        public int GameSessionId { get; set; }
        public DateTime GameSessionDate { get; set; }
        public DateTime GameSessionEndTime { get; set; }
        public bool IsDelete { get; set; } = false;
        public int TableId { get; set; }
        public int EventId { get; set; }
        public int MasterId { get; set; }
        public TableOutputDTO? Table { get; set; }
        public EventOutputDTO? Event { get; set; }
        //public ApplicationUserOutputDTO? Master { get; set; }
        public List<ReservationOutputDTO>? Reservations { get; set; }
    }
}
