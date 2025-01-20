using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupHeroApp.BusinessLogic
{
    public interface IDatabaseBackupManager
    {
        void BackupDatabase(string serverName, string databaseName, string backupDirectory, int retentionDays);
        void DeleteOldBackup(string backupDirectory, string databaseName, int retentionDays);
    }
}
