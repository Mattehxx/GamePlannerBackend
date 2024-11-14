namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class UserOutputDTO
    {
        public required string Id { get; set; }
        public required string? Email { get; set; }
        public required string? ImgUrl { get; set; }
        public required int Level { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public List<PreferenceOutputDTO>? Preferences { get; set; }
    }
}