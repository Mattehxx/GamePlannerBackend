using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;

namespace GamePlanner.DTO.InputDTO
{
    public class GameSessionInputDTO
    {
        public required DateTime GameSessionDate { get; set; }
        public required DateTime GameSessionEndTime { get; set; }
        public bool IsDelete { get; set; } = false;
        public required int TableId { get; set; }
        public required int EventId { get; set; }
        public required string MasterId { get; set; }
        //public TableInputDTO? Table { get; set; }
        //public EventInputDTO? Event { get; set; }
        //public ApplicationUserInputDTO? Master { get; set; }
        //public List<ReservationInputDTO>? Reservations { get; set; }
    }
}
