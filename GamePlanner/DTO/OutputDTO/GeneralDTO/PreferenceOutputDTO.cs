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
        public UserOutputDTO? User { get; set; }
        public KnowledgeOutputDTO? Knowledge { get; set; }
        public GameOutputDTO? Game { get; set; }
    }
}
