using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class GameDetailsDTO : GameOutputDTO
    {
        public required string Description { get; set; }
        public List<SessionOutputDTO>? Sessions { get; set; }
        public List<PreferenceOutputDTO>? Preferences { get; set; }
    }
}
