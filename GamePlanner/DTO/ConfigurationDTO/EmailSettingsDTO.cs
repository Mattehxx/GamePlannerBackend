namespace GamePlanner.DTO.ConfigurationDTO
{
    public class EmailSettingsDTO
    {
        public required string SMPTServer { get; set; }
        public required int SMPTPort { get; set; }
        public required string EmailAddress { get; set; }
        public required string EmailPassword { get; set; }
    }
}
