namespace SmartInventoryBE.Settings
{
    public class GeneralSettings
    {
        public const string SectionName = "GeneralSettings";
        public int BatchSize { get; set; } = 100;
        public bool EnableDbLogging { get; set; } = true;
    }
}
