using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DTO.InputDTO
{
    public class PreferenceInputDTO
    {
        public required bool CanBeMaster { get; set; }
        public required bool IsDeleted { get; set; }
        public required string UserId { get; set; }
        public required int KnowledgeId { get; set; }
        public required int GameId { get; set; }
    }
}
