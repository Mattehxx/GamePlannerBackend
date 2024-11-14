namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class ReservationOutputDTO
    {
        public required int ReservationId { get; set; }
        public required bool IsConfirmed { get; set; }
        public required bool IsDelete { get; set; } = false;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public required int SessionId { get; set; }
    }
}