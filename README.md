## BackupHero Documentation

### Overview
BackupHero is a utility tool designed to automate the process of backing up specific databases. This application schedules and performs backups, ensuring data is securely saved and retained according to specified criteria.

### Prerequisites

To run BackupHeroApp, please ensure the following prerequisites are installed on your system:

1. **Operating System**: Windows 7 or later
2. **.NET Framework**: .NET Framework 4.7.2 or later
3. **SQL Server**: SQL Server 2005
4. **Disk Space**: Sufficient disk space to accommodate backups (at least 2.5 times the size of the databases being backed up)

### Installation

1. **Download the Installer**:
   - Obtain the installer file (BackupHeroSetup.exe) from the provided source.
   
2. **Run the Installer**:
   - Double-click the installer file and follow the on-screen instructions to install the application.
   
3. **Installation Directory**:
   - By default, the application is installed in `C:\Program Files (x86)\Vegasoft\BackupHeroApp`.

### Using the Application

1. **Launch the Application**:
   - Open BackupHero from the Start Menu or desktop shortcut.
   
2. **Schedule a Backup**:
   - Select database, backup time, and maximum number of backup copies.
   - Click the "Start Auto Backup" button to create a scheduled task.

3. **Task Scheduler**:
   - The application uses Task Scheduler to run backup tasks. The scheduled task is named `DatabaseBackup_<databaseName>` and is set to run daily at the specified time.
   - Ensure the task runs as soon as possible if a scheduled start is missed.

4. **BackupConsole**:
   - `BackupConsole.exe` is a command-line tool that performs the actual backup operation. It is called by the scheduled task with the necessary arguments.

### Behavior in Different Scenarios

1. **Missed Backups**:
   - If the computer is powered off during the scheduled backup time, the task will run as soon as the computer is powered back on.

2. **Backup Directory Not Accessible**:
   - If the specified backup directory is not accessible, an error message is logged, and the task will not proceed.

3. **Database Connection Issues**:
   - If the application cannot connect to the specified server or database, an error message is displayed, and the issue is logged.

4. **Computer in Sleep Mode**:
   - The task is configured to wake the computer to perform the backup.

5. **Insufficient Disk Space**:
    - Before performing the backup, the application checks if there is enough disk space available (at least 2.5 times the size of the database)

### Validations and Error Handling

1. **Input Validations**:
   - **Database Name**: Ensures the database name is not empty and exists on the specified server.
   - **Backup Directory**: Checks if the backup directory exists. If not, it attempts to create it.
   - **Backup Time**: Ensures the backup time is not empty and is in a valid format.
   - **Maximum Copies**: Validates that the maximum copies are a positive integer.

2. **Error Logging**:
   - All errors and important events are logged to a log file specified in the configuration settings. (by default `C:\softmed2\DatabaseBackups\backup_log.txt`.)
   - Logs include details about the error, such as the time of occurrence and the nature of the issue.

3. **Task Scheduler Settings**:
   - **Start When Available**: Ensures the task runs if missed due to the computer being powered off.
   - **Wake to Run**: Ensures the task wakes the computer to perform the backup if it's in sleep mode.
