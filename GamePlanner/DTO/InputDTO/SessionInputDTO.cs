namespace GamePlanner.DTO.InputDTO
{
    public class SessionInputDTO
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Seats { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public string? MasterId { get; set; }
        public required int EventId { get; set; }
        public required int GameId { get; set; }
    }
}
