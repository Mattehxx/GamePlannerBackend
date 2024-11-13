using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class EventDetailsDTO : EventOutputDTO
    {
        public required string Description { get; set; }
        //public List<Session>? Sessions { get; set; }
    }
}
