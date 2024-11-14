using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class SessionDetailsDTO : SessionOutputDTO
    {
        public required int TotalSeats { get; set; }
        public required int AvailableSeats { get; set; }
        public required int QueueLength { get; set; }
        public UserOutputDTO? Master { get; set; }
        public List<ReservationOutputDTO>? Reservations { get; set; }
    }
}
