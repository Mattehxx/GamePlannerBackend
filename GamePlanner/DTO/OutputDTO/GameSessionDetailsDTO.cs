namespace GamePlanner.DTO.OutputDTO
{
    public class GameSessionDetailsDTO
    {
        public int GameSessionId { get; set; }
        public DateTime GameSessionDate { get; set; }
        public DateTime GameSessionEndTime { get; set; }
        public bool IsDelete { get; set; } = false;
        public int TableId { get; set; }
        public int EventId { get; set; }
        public string MasterId { get; set; }
        public TableOutputDTO? Table { get; set; }
        //public List<ReservationOutputDTO>? Reservations { get; set; }
        //dettagli
        public string? MasterName { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsReservable { get; set; }
        public int QueueLength { get; set; }
    }
}
