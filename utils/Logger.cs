using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace BackupHeroApp.Utils
{
    public static class Logger
    {
        private static readonly string logFilePath = ConfigurationManager.AppSettings["LogFilePath"];

        public static void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
