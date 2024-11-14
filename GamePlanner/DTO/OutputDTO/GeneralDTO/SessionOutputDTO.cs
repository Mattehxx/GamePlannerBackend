namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class SessionOutputDTO
    {
        public required int SessionId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public EventOutputDTO? Event { get; set; }
        public GameOutputDTO? Game { get; set; }
    }
}
