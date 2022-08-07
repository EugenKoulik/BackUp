using Backup.Configuration;
using Backup.Utils;

namespace Backup
{
    public class BackUp
    {
        private readonly BackUpSettings _settings;
        private readonly ISerializer _serializer;
        private readonly BackUpHandler _backUpHandler;
        public BackUp()
        {
            _serializer = new NewtonsoftJsonSerializer();
            _settings = GetSettingsFromJson(_serializer);
            _backUpHandler = new BackUpHandler();
        }
        public BackUp(ISerializer serializer)
        {
            _settings = GetSettingsFromJson(serializer);
            _backUpHandler = new BackUpHandler();
        }
        public void Start()
        {
            _backUpHandler.GetBackUp(_settings);
        }
        private BackUpSettings GetSettingsFromJson(ISerializer serializer)
        {
            using var reader = new StreamReader(Path.Combine("Configuration", "BackUpSettings.json"));
            var jsonString = reader.ReadToEnd();
            return _serializer.Deserialize<BackUpSettings>(jsonString);
        }
    }
}

