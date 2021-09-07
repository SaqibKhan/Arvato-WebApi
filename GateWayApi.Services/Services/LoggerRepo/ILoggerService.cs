using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayApi.DAL.Entity;


namespace GateWayApi.Services.LoggerRepo
{
    public interface ILoggerService
    {
        void AddToLogs(LogItem log);

        IEnumerable<LogItem> GetAllLogs();
    }
}
