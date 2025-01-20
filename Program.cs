using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackupHeroApp.BusinessLogic;
using BackupHeroApp.DataAccess;
using BackupHeroApp.UI;

namespace BackupHeroApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create instances of the required services
            IServiceManager serviceManager = new ServiceManager();
            IDatabaseHelper databaseHelper = new DatabaseHelper();
            ISqlServerChecker sqlServerChecker = new SqlServerChecker();

            Application.Run(new Form1(serviceManager, databaseHelper, sqlServerChecker));
        }
    }
}
