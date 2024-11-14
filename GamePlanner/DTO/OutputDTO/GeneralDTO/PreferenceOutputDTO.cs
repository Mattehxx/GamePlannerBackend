using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class PreferenceOutputDTO
    {
        public required int PreferenceId { get; set; }
        public required bool CanBeMaster { get; set; }
        public required bool IsDeleted { get; set; }
        public required string UserId { get; set; }
        public required int KnowledgeId { get; set; }
        public required int GameId { get; set; }
        public string? GameName { get; set; }
        public string? KnowledgeName { get; set; }
    }
}
