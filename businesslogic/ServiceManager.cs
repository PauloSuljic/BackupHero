using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;
using System;

namespace BackupHeroApp.BusinessLogic
{
    public class ServiceManager : IServiceManager
    {

        public bool IsSqlServerServiceRunning()
        {
            // Check if the SQL Server service is running
            ServiceController sqlService = new ServiceController("MSSQL$SQLEXPRESS"); // Replace with your service name if different
            try
            {
                return sqlService.Status == ServiceControllerStatus.Running;
            }
            catch
            {
                return false;
            }
        }

        public void StartSqlServerService()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("net", "start MSSQL$SQLEXPRESS")
                {
                    UseShellExecute = true,
                    Verb = "runas" // This ensures the process runs with elevated privileges
                };

                Process process = new Process { StartInfo = processStartInfo };
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Failed to start SQL Server service. Exit code: {process.ExitCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start SQL Server service: {ex.Message}");
            }
        }

    }
}
