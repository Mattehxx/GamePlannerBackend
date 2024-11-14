namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class GameOutputDTO
    {
        public required int GameId { get; set; }
        public required string Name { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDisabled { get; set; }
        public required bool IsDeleted { get; set; } = false;
        
    }
}
