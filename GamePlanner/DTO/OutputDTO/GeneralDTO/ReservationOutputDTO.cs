namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class ReservationOutputDTO
    {
        public required int ReservationId { get; set; }
        public required bool IsConfirmed { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required int SessionId { get; set; }
        public required string UserId { get; set; }
        public SessionOutputDTO? Session { get; set; }
        public UserOutputDTO? User { get; set; }
    }
}