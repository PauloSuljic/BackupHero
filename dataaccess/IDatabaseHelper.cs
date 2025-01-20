using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupHeroApp.DataAccess
{
    public interface IDatabaseHelper
    {
        List<string> LoadDatabases(string serverName);
    }
}
