using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BackupHeroApp.BusinessLogic;
using BackupHeroApp.Utils;
using BackupHeroApp.DataAccess;
using System.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32.TaskScheduler;
using System.Linq;

namespace BackupHeroApp.UI
{
    public partial class Form1 : Form
    {
        private List<string> scheduledDatabases = new List<string>();

        private DatabaseBackupManager backupManager;
        private IServiceManager serviceManager;
        private IDatabaseHelper databaseHelper;
        private ISqlServerChecker sqlServerChecker;

        // Configuration settings
        private string logFilePath;
        private string backupDirectory;
        private string serverName;
        private string sqlInstanceName;

        public Form1(IServiceManager serviceManager, IDatabaseHelper databaseHelper, ISqlServerChecker sqlServerChecker)
        {
            InitializeComponent();
            this.serviceManager = serviceManager;
            this.databaseHelper = databaseHelper;
            this.sqlServerChecker = sqlServerChecker;
            backupManager = new DatabaseBackupManager();

            // Load configuration settings
            logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            backupDirectory = ConfigurationManager.AppSettings["BackupDirectory"];
            serverName = ConfigurationManager.AppSettings["SqlServerName"];
            sqlInstanceName = ConfigurationManager.AppSettings["SqlInstanceName"];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckSqlServer();
            LoadDatabases();
            CheckRunningTasks();
        }

        private void buttonBackup_Click(object sender, EventArgs e)
        {
            if (dateTimePickerBackupTime.Value == null)
            {
                MessageBox.Show("Please select a backup time.");
                return;
            }
            if (numericUpDownMaxCopies.Value <= 0)
            {
                MessageBox.Show("Please enter a valid number of max copies.");
                return;
            }

            // Existing logic
            string selectedDatabase = comboBoxDatabases.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedDatabase))
            {
                MessageBox.Show("Please select a database.");
                return;
            }

            if (!CheckDiskSpace("C:\\", selectedDatabase))
            {
                MessageBox.Show("Insufficient disk space. Please ensure there is at least 2.5 times the free space of the database size.");
                return;
            }

            DateTime backupTime = dateTimePickerBackupTime.Value;
            int maxCopies = (int)numericUpDownMaxCopies.Value;

            labelStatus.Text = $"'{selectedDatabase}' is set to auto backup.";
            labelStatus.ForeColor = System.Drawing.Color.Green;

            TaskManager.DeleteTask($"DatabaseBackup_{selectedDatabase}");

            // Perform immediate backup
            Logger.Log("INFO Auto backup started for database: " + selectedDatabase);
            backupManager.BackupDatabase(serverName, selectedDatabase, backupDirectory, maxCopies);

            // Schedule the task based on mode
            string mode = ConfigurationManager.AppSettings["Mode"];
            if (mode == "Test")
            {
                ScheduleTestBackup(serverName, selectedDatabase, backupDirectory, maxCopies);
            }
            else
            {
                ScheduleProdBackup(serverName, selectedDatabase, backupDirectory, backupTime, maxCopies);
            }

            string displayText = mode == "Test" ? $"{selectedDatabase} [Max Copies: {maxCopies}]" : $"{selectedDatabase} [Max Copies: {maxCopies}]";

            // Find the index of the existing database entry, if it exists
            int existingIndex = scheduledDatabases.FindIndex(item => item.Contains(selectedDatabase));
            if (existingIndex != -1)
            {
                scheduledDatabases.RemoveAt(existingIndex);
            }

            scheduledDatabases.Add(displayText);
            UpdateScheduledDatabaseList();

        }

        private void buttonStopBackup_Click(object sender, EventArgs e)
        {
            string selectedDatabase = listBoxScheduledDatabases.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedDatabase))
            {
                MessageBox.Show("Please select a database to stop the backup.");
                return;
            }

            // Extract the database name from the selected item (removing the max copies part)
            string dbName = selectedDatabase.Split('[')[0].Trim();
            string taskName = $"DatabaseBackup_{dbName}";

            using (TaskService ts = new TaskService())
            {
                var task = ts.FindTask(taskName);
                if (task != null)
                {
                    ts.RootFolder.DeleteTask(taskName);
                    // Remove the selected item from the list
                    listBoxScheduledDatabases.Items.Remove(selectedDatabase);
                    scheduledDatabases.Remove(selectedDatabase);
                }
                else
                {
                    MessageBox.Show("Task not found. It may have already been deleted.");
                }
            }

            Logger.Log("OK Auto backup stopped for database: " + dbName);
            labelStatus.Text = $"Auto backup stopped for database: " + dbName;
            labelStatus.ForeColor = System.Drawing.Color.Green;
        }

        private void CheckRunningTasks()
        {
            using (TaskService ts = new TaskService())
            {
                var tasks = ts.AllTasks.Where(t => t.Name.StartsWith("DatabaseBackup")).ToList();
                scheduledDatabases.Clear();
                foreach (var task in tasks)
                {
                    string dbName = task.Name.Replace("DatabaseBackup_", "");
                    string description = task.Definition.RegistrationInfo.Description;
                    string maxCopies = "Unknown";

                    if (description.Contains("Max Copies:"))
                    {
                        int startIndex = description.IndexOf("Max Copies:") + "Max Copies:".Length;
                        int endIndex = description.IndexOf(")", startIndex);
                        if (endIndex > startIndex)
                        {
                            maxCopies = description.Substring(startIndex, endIndex - startIndex).Trim();
                        }
                    }

                    string displayText = $"{dbName} [{maxCopies}]";
                    if (!scheduledDatabases.Contains(displayText))
                    {
                        scheduledDatabases.Add(displayText);
                    }
                }
                UpdateScheduledDatabaseList();
            }
        }

        private void CheckSqlServer()
        {
            // Check if SQL Server is installed
            bool isSqlServerInstalled = sqlServerChecker.IsSqlServerInstalled();
            if (!isSqlServerInstalled)
            {
                MessageBox.Show("SQL Server is not installed. This application cannot run on this computer.");
                this.Close();
                return;
            }

            // Check if the SQL Server service is running
            if (!serviceManager.IsSqlServerServiceRunning())
            {
                DialogResult result = MessageBox.Show("SQL Server service is not running. Would you like to start it?", "Service Not Running", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    serviceManager.StartSqlServerService();
                    if (!serviceManager.IsSqlServerServiceRunning())
                    {
                        MessageBox.Show("Failed to start SQL Server service.");
                        this.Close();
                        return;
                    }
                }
                else
                {
                    this.Close();
                    return;
                }
            }
        }

        private void LoadDatabases()
        {
            try
            {
                string computerName = Environment.MachineName;
                string instanceName = ConfigurationManager.AppSettings["SqlInstanceName"];
                string fullServerName = $".\\SQLEXPRESS";

                var databases = databaseHelper.LoadDatabases(fullServerName);
                comboBoxDatabases.Items.Clear();
                foreach (var db in databases)
                {
                    comboBoxDatabases.Items.Add(db);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("FAILED Error loading databases: " + ex.Message);
                MessageBox.Show("An error occurred while loading databases. Please check the logs for more details.");
            }
        }

        private bool CheckDiskSpace(string driveLetter, string databaseName)
        {
            Server server = new Server(new ServerConnection(serverName));
            Database database = server.Databases[databaseName];
            double databaseSize = database.Size * 1024 * 1024; // Convert from MB to Bytes
            long requiredSpace = (long)(databaseSize * 2.5);

            DriveInfo driveInfo = new DriveInfo(driveLetter);
            long freeSpace = driveInfo.AvailableFreeSpace;

            return freeSpace >= requiredSpace;
        }

        private void ScheduleTestBackup(string serverName, string databaseName, string backupDirectory, int maxCopies)
        {
            try
            {
                string taskName = "DatabaseBackup_" + databaseName;
                using (TaskService ts = new TaskService())
                {
                    var existingTask = ts.FindTask(taskName);
                    if (existingTask != null)
                    {
                        ts.RootFolder.DeleteTask(taskName);
                    }

                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = $"Backup for database {databaseName} (Max Copies: {maxCopies})";

                    // Set the trigger to run every minute for testing
                    TimeTrigger timeTrigger = new TimeTrigger();
                    timeTrigger.StartBoundary = DateTime.Now + TimeSpan.FromMinutes(1);
                    timeTrigger.Repetition.Interval = TimeSpan.FromMinutes(1);
                    td.Triggers.Add(timeTrigger);

                    Logger.Log("INFO Scheduled minute interval backup task for database: " + databaseName);

                    string exePath = System.IO.Path.Combine(Application.StartupPath, @"..\..\..\BackupConsole\bin\Debug\BackupConsole.exe"); 
                    string arguments = $"{serverName} {databaseName} {backupDirectory} {maxCopies}";
                    td.Actions.Add(new ExecAction(exePath, arguments, null));

                    ts.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Error scheduling backup: {ex.Message}";
                labelStatus.ForeColor = System.Drawing.Color.Red;
                Logger.Log("FAILED Error scheduling backup for database: " + databaseName + " - " + ex.Message);
            }
        }

        private void ScheduleProdBackup(string serverName, string databaseName, string backupDirectory, DateTime backupTime, int maxCopies)
        {
            try
            {
                string taskName = "DatabaseBackup_" + databaseName;
                using (TaskService ts = new TaskService())
                {
                    var existingTask = ts.FindTask(taskName);
                    if (existingTask != null)
                    {
                        ts.RootFolder.DeleteTask(taskName);
                    }

                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = $"Backup for database {databaseName} (Max Copies: {maxCopies})";

                    DailyTrigger dailyTrigger = new DailyTrigger
                    {
                        StartBoundary = DateTime.Today + backupTime.TimeOfDay
                    };
                    td.Triggers.Add(dailyTrigger);

                    Logger.Log("INFO Scheduled backup task for database: " + databaseName + " at " + backupTime.ToShortTimeString());

                    // Determine the correct path to BackupConsole.exe based on the execution context
#if DEBUG
            string exePath = Path.Combine(Application.StartupPath, @"..\..\..\BackupConsole\bin\Debug\BackupConsole.exe");
#else
                    string exePath = Path.Combine(Application.StartupPath, "BackupConsole.exe");
#endif

                    string arguments = $"{serverName} {databaseName} {backupDirectory} {maxCopies}";

                    td.Actions.Add(new ExecAction(exePath, arguments, null));

                    // Ensure task runs if missed
                    td.Settings.StartWhenAvailable = true;
                    td.Settings.WakeToRun = true;

                    ts.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Error scheduling backup: {ex.Message}";
                labelStatus.ForeColor = System.Drawing.Color.Red;
                Logger.Log("FAILED Error scheduling backup for database: " + databaseName + " - " + ex.Message);
            }
        }

        private void UpdateScheduledDatabaseList()
        {
            listBoxScheduledDatabases.Items.Clear();
            foreach (string dbName in scheduledDatabases)
            {
                listBoxScheduledDatabases.Items.Add(dbName);
            }
        }
    }
}
