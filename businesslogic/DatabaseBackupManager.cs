using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Windows.Forms;
using BackupHeroApp.Utils;

namespace BackupHeroApp.BusinessLogic
{
    public class DatabaseBackupManager : IDatabaseBackupManager
    {
        public void BackupDatabase(string serverName, string databaseName, string backupDirectory, int retentionDays)
        {
            try
            {
                Server server = new Server(new ServerConnection(serverName));
                Database database = server.Databases[databaseName];

                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                string backupFileName = Path.Combine(backupDirectory, $"{databaseName}_Backup_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.bak");

                Backup backup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = databaseName
                };
                backup.Devices.AddDevice(backupFileName, DeviceType.File);
                backup.Initialize = true;
                backup.SqlBackup(server);

                MessageBox.Show($"Auto backup for '{databaseName}' started successfully.");
                DeleteOldBackup(backupDirectory, databaseName, retentionDays);
                Logger.Log("OK Initial backup completed for database: " + databaseName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during backup: {ex.Message}");
                Logger.Log("FAILED Error during backup for database: " + databaseName + " - " + ex.Message);
            }
        }

        public void DeleteOldBackup(string backupDirectory, string databaseName, int maxCopies)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(backupDirectory);
                FileInfo[] backupFiles = directoryInfo.GetFiles($"{databaseName}_Backup_*.bak");
                Array.Sort(backupFiles, (x, y) => y.CreationTime.CompareTo(x.CreationTime));

                // Keep only the latest 'retentionDays' number of backup files
                for (int i = maxCopies; i < backupFiles.Length; i++)
                {
                    backupFiles[i].Delete();
                    Console.WriteLine($"Deleted old backup file: {backupFiles[i].Name}");
                    Logger.Log("OK  Old backups cleaned up for database: " + databaseName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during cleanup: {ex.Message}");
                Logger.Log("FAILED Error during cleanup for database: " + databaseName + " - " + ex.Message);
            }
        }

    }
}
