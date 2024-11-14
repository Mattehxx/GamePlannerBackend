using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO.OutputDTO.DetailDTO
{
    public class SessionDetailsDTO : SessionOutputDTO
    {
        public required int Seats { get; set; }
        public required int EventId { get; set; }
        public required int GameId { get; set; }
        public UserOutputDTO? Master { get; set; }
        public List<ReservationOutputDTO>? Reservations { get; set; }
    }
}
