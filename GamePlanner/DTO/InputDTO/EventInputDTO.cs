namespace GamePlanner.DTO.InputDTO
{
    public class EventInputDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Duration { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required string AdminId { get; set; }
    }
}