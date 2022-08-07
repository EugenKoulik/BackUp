namespace Backup.Utils
{
    public class BackUpLogger 
    {
        private readonly string _pathToFile;
        private const string FileName =  "Log.txt";

        public BackUpLogger(string pathToFile)
        {
            _pathToFile = pathToFile;
        }
        
        public void Info(string info)
        {
            var path = Path.Combine(_pathToFile, FileName);
            using var writer = new StreamWriter(path, true, System.Text.Encoding.Default);
            writer.WriteAsync($"Info:\n{info}\n");
        }

        public void Error(string info)
        {
            var path = Path.Combine(_pathToFile, FileName);
            using var writer = new StreamWriter(path, true, System.Text.Encoding.Default);
            writer.WriteAsync($"Error:\n{info}\n");
        }

        public void Debug(string info)
        {
            var path = Path.Combine(_pathToFile, FileName);
            using var writer = new StreamWriter(path, true, System.Text.Encoding.Default);
            writer.WriteAsync($"Debug:\n{info}\n");
        }
    }
}

