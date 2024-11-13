namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class TableOutputDTO
    {
        public required int TableId { get; set; }
        public required string Name { get; set; }
        public required int Seat { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public List<GameSessionOutputDTO>? gameSessions { get; set; }
    }
}