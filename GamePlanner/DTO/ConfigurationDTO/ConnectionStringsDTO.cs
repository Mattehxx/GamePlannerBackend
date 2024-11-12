namespace GamePlanner.DTO.ConfigurationDTO
{
    public class ConnectionStringsDTO
    {
        public required string DbConnection { get; set; }
        public required string StorageAccountConnection { get; set; }
    }
}
