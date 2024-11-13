namespace GamePlanner.DTO.InputDTO
{
    public class ReservationInputDTO
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required DateTime BirthDate { get; set; }
        public required bool IsConfirmed { get; set; }
        public required bool IsDelete { get; set; } = false;
        public string? UserId { get; set; }
        public required int GameSessionId { get; set; }
        public required bool IsQueued { get; set; }
    }
}
