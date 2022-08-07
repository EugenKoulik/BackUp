using System.Security;
using Backup.Configuration;
using Backup.Utils;

namespace Backup
{
    public class BackUpHandler
    {
        private BackUpLogger? _log;
        public void GetBackUp(BackUpSettings settings)
        {
            var currentDateTime = DateTime.Now.ToString(settings.DateTimeFormat);
            var targetFolder = Path.Combine(settings.TargetFolder, currentDateTime);
            _log = new BackUpLogger(targetFolder);

            foreach (var currentSourceFolder in settings.SourceFolders)
            {
                if (Directory.Exists(currentSourceFolder))
                {
                    try
                    {
                        CopyFolder(currentSourceFolder, targetFolder);
                    }
                    catch (ArgumentException argumentException) {_log.Error("");}
                    catch (NotSupportedException notSupportedException) {_log.Error("");}
                    catch (SecurityException securityException) {_log.Error("");}
                    catch (IOException ioException) {_log.Error("");}
                    catch (UnauthorizedAccessException unauthorizedAccessException) {_log.Error("");}
                }
                else
                {
                    _log.Error("");
                    _log.Debug("");
                    _log.Info("");
                }
            }
        }

        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                _log!.Debug("");
                Directory.CreateDirectory(destFolder);
                _log!.Info("");
            }
            
            var files = Directory.GetFiles(sourceFolder);

            foreach (var file in files)
            {
                _log!.Debug("");
                File.Copy(file, Path.Combine(destFolder, Path.GetFileName(file)));
                _log!.Info("");
            }
            
            var folders = Directory.GetDirectories(sourceFolder);

            foreach (var folder in folders)
            {
                _log!.Debug("");
                CopyFolder(folder, Path.Combine(destFolder, Path.GetFileName(folder)));
                _log!.Info("");
            }
        }
    }
}

