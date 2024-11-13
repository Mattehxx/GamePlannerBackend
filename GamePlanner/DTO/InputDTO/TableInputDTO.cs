namespace GamePlanner.DTO.InputDTO
{
    public class TableInputDTO
    {
        public required string Name { get; set; }
        public required int Seat { get; set; }
        public required bool IsDeleted { get; set; } = false;
    }
}
