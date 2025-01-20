using Microsoft.Win32;

namespace BackupHeroApp.DataAccess
{
    public class SqlServerChecker : ISqlServerChecker
    {
        public bool IsSqlServerInstalled()
        {
            // Check registry for SQL Server 2005 installation
            string sqlServer2005Key = @"SOFTWARE\Microsoft\Microsoft SQL Server\90\Tools\ClientSetup";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(sqlServer2005Key))
            {
                if (key != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
