using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class EventDetailsDTO : EventOutputDTO
    {
        public required string EventDescription { get; set; }
        public int AvailableTables { get { return GameSessionsGeneral != null ? GameSessionsGeneral.Count() : 0; } }
        public List<GameSessionOutputDTO>? GameSessionsGeneral { get; set; }
    }
}
