using System.Collections.Generic;
using GateWayApi.DAL.Entity;


namespace GateWayApi.Shared.Interfaces.LoggerService
{
    public interface ILoggerService
    {
        void AddToLogs(LogItem log);

        IEnumerable<LogItem> GetAllLogs();
    }
}
