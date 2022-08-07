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
            if (!Directory.Exists(settings.TargetFolder))
            {
                Console.WriteLine("Destination folder not found!");
                return;
            }
            _log = new BackUpLogger(settings.TargetFolder, settings, currentDateTime);
            
            foreach (var currentSourceFolder in settings.SourceFolders)
            {
                if (Directory.Exists(currentSourceFolder))
                {
                    try
                    {
                        CopyFolder(currentSourceFolder, targetFolder);
                    }
                    catch (ArgumentException argumentException) {_log.Error(argumentException.Message);}
                    catch (NotSupportedException notSupportedException) {_log.Error(notSupportedException.Message);}
                    catch (SecurityException securityException) {_log.Error(securityException.Message);}
                    catch (IOException ioException) {_log.Error(ioException.Message);}
                    catch (UnauthorizedAccessException unauthorizedAccessException) {_log.Error(unauthorizedAccessException.Message);}
                }
                else
                {
                    _log.Error($"File {currentSourceFolder} not exist!");
                }
            }
        }

        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                _log!.Debug($"Trying to create a {destFolder} directory");
                Directory.CreateDirectory(destFolder);
                _log!.Info($"Directory {destFolder} successfully created");
            }
            
            var files = Directory.GetFiles(sourceFolder);

            foreach (var file in files)
            {
                _log!.Debug($"Trying to copy a {file} in {destFolder} directory");
                File.Copy(file, Path.Combine(destFolder, Path.GetFileName(file)));
                _log!.Info($"File {file} copied successfully to {destFolder} folder");
            }
            
            var folders = Directory.GetDirectories(sourceFolder);

            foreach (var folder in folders)
            {
                _log!.Debug($"Trying to copy from {folder} to {destFolder} directory");
                CopyFolder(folder, Path.Combine(destFolder, Path.GetFileName(folder)));
                _log!.Info($"Folder {folder} successfully copied to {destFolder} folder");
            }
        }
    }
}

