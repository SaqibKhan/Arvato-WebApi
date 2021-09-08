using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using GateWayApi.DAL.Entity;
using GateWayApi.Shared.Interfaces.LoggerService;

namespace GateWayApi.DAL.Controllers
{
    [ApiController]
    [Route("[controller]")]
  public class LoggerServiceController : ControllerBase
    {
        private readonly ILogger<LoggerServiceController> _logger;
        private readonly ILoggerService _loggerService;

        public LoggerServiceController(ILogger<LoggerServiceController> logger, ILoggerService loggerService)
        {
            _logger = logger;
            _loggerService = loggerService;
            
        }


        [HttpGet]
        [Route("GetAllLogs")]
        public  IEnumerable<LogItem> GetAllLogs()
        {
            IEnumerable<LogItem> result = null;
            try
            {
                result =  _loggerService.GetAllLogs();

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error Message: {ex.Message}{Environment.NewLine} StackTrace: {ex.StackTrace}";
                _logger.LogWarning(errorMessage);
            }

            return result;
        }
    }
}
