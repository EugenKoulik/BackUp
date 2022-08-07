using Backup.Configuration;

namespace Backup.Utils
{
    public class BackUpLogger 
    {
        private readonly string _pathToFile;
        private readonly string _fileName;
        private readonly BackUpSettings _backUpSettings;

        public BackUpLogger(string pathToFile, BackUpSettings settings, string? currentDateTime)
        {
            _pathToFile = pathToFile;
            _backUpSettings = settings;
            _fileName = Path.Combine(_pathToFile, currentDateTime + "log.txt");
        }
        
        public void Info(string info)
        {
            if (!_backUpSettings.LoggingParams.Info)
                return;
            var path = Path.Combine(_pathToFile, _fileName);
            using var writer = new StreamWriter(path, true, System.Text.Encoding.Default);
            writer.WriteAsync($"Info:\n{info}\n");
        }

        public void Error(string info)
        {
            if (!_backUpSettings.LoggingParams.Error)
                return;
            var path = Path.Combine(_pathToFile, _fileName);
            using var writer = new StreamWriter(path, true, System.Text.Encoding.Default);
            writer.WriteAsync($"Error:\n{info}\n");
        }

        public void Debug(string info)
        {
            if (!_backUpSettings.LoggingParams.Debug)
                return;
            var path = Path.Combine(_pathToFile, _fileName);
            using var writer = new StreamWriter(path, true, System.Text.Encoding.Default);
            writer.WriteAsync($"Debug:\n{info}\n");
        }
    }
}

