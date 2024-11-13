using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class RecurrenceDetailsDTO : RecurrenceOutputDTO
    {
        public List<EventOutputDTO>? Events { get; set; }
    }
}
