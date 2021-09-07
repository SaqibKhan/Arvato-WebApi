using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayApi.DAL.Entity;
using GateWayApi.DAL.Repository;
using GateWayApi.Services.LoggerRepo;

namespace GateWayApi.Services.Services
{
    public class LoggerService: ILoggerService
    {
       private readonly IGenericRepository<LogItem> _logRepository = null;

        public LoggerService(IGenericRepository<LogItem> repository)
        {
            this._logRepository = repository;
        }

        public void AddToLogs(LogItem logItem)
        {
            _logRepository.Insert(logItem);
            _logRepository.Save();
        }

        public IEnumerable<LogItem> GetAllLogs()
        {
            return _logRepository.GetAll();
        }
    }
}
