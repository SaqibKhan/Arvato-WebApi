using System.Collections.Generic;
using GateWayApi.DAL.Entity;


namespace GateWayApi.Services.LoggerRepo
{
    public interface ILoggerService
    {
        void AddToLogs(LogItem log);

        IEnumerable<LogItem> GetAllLogs();
    }
}
