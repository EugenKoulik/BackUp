namespace Backup.Configuration
{
    public class BackUpSettings
    {
        public string[] SourceFolders { get; set; }
        public string TargetFolder { get; set; }
        public LoggingParams LoggingParams { get; set; }
        public string DateTimeFormat { get; set; }
    }
}
