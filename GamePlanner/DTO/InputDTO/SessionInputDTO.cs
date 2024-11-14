namespace GamePlanner.DTO.InputDTO
{
    public class SessionInputDTO
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required int TableId { get; set; }
        public required int EventId { get; set; }
        public required string MasterId { get; set; }
        public required int GameId { get; set; }
        public required int Seats { get; set; }
    }
}
