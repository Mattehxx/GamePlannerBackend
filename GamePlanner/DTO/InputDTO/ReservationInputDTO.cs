namespace GamePlanner.DTO.InputDTO
{
    public class ReservationInputDTO
    {
        public required bool IsConfirmed { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required int SessionId { get; set; }
        public required string UserId { get; set; }
    }
}
