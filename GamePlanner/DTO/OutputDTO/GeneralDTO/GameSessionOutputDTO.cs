namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class GameSessionOutputDTO
    {
        public required int GameSessionId { get; set; }
        public required DateTime GameSessionStartDate { get; set; }
        public required DateTime GameSessionEndDate { get; set; }
        public required bool IsDelete { get; set; } = false;
        public required int TableId { get; set; }
        public required int EventId { get; set; }
        public TableOutputDTO? Table { get; set; }
        public EventOutputDTO? Event { get; set; }
    }
}
