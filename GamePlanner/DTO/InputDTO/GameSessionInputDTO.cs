namespace GamePlanner.DTO.InputDTO
{
    public class GameSessionInputDTO
    {
        public required DateTime GameSessionStartDate { get; set; }
        public required DateTime GameSessionEndDate { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required int TableId { get; set; }
        public required int EventId { get; set; }
        public required string MasterId { get; set; }
    }
}
