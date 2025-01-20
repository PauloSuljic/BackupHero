using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Windows.Forms;
using BackupHeroApp.Utils;

namespace BackupHeroApp.DataAccess
{
    public class DatabaseHelper : IDatabaseHelper
    {
        public List<string> LoadDatabases(string serverName)
        {
            List<string> databases = new List<string>();
            try
            {
                Server server = new Server(new ServerConnection(".\\SQLEXPRESS"));
                foreach (Database db in server.Databases)
                {
                    if (db.Name.StartsWith("Softmed") || db.Name.StartsWith("Specijalistika"))
                    {
                        databases.Add(db.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return databases;
        }
    }
}
