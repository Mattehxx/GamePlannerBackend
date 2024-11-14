namespace GamePlanner.DTO.ConfigurationDTO
{
    public class EmailSettingsDTO
    {
        public string SMPTServer { get; set; }
        public int SMPTPort { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
    }
}
