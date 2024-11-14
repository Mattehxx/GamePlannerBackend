using GamePlanner.DAL.Data.Auth;

namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class EventOutputDTO
    {
        public required int EventId { get; set; }
        public required string Name { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public ApplicationUser? Admin { get; set; }
    }
}