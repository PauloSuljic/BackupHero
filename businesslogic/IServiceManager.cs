using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupHeroApp.BusinessLogic
{
    public interface IServiceManager
    {
        bool IsSqlServerServiceRunning();
        void StartSqlServerService();
    }
}
