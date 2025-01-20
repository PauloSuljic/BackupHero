using BackupHeroApp.Utils;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;

namespace BackupHeroApp.BusinessLogic
{
    public static class TaskManager
    {
        public static void DeleteTask(string taskName)
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    var task = ts.FindTask(taskName);
                    if (task != null)
                    {
                        ts.RootFolder.DeleteTask(taskName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Update the status label if needed
                Logger.Log("FAILED Error deleting task: " + taskName + " - " + ex.Message);
            }
        }
    }
}
