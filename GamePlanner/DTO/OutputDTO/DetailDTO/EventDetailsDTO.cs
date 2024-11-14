using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class EventDetailsDTO : EventOutputDTO
    {
        //public required string EventDescription { get; set; }
        public int AvailableTables { get { return SessionsDetails != null ? SessionsDetails.Count() : 0; } }
        public List<SessionDetailsDTO>? SessionsDetails { get; set; }
    }
}
