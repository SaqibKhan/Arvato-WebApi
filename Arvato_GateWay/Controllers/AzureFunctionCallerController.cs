using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GateWayApi.DAL.ActionFilter;
using GateWayApi.DAL.Entity;
using GateWayApi.Shared.AzureFunCaller;
using Microsoft.AspNetCore.Authorization;

namespace GateWayApi.DAL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequestRateLimit(Name = "Limit Request Number", Seconds = 1)]
    public class AzureFunctionCallerController : ControllerBase
    {
        private readonly ILogger<LoggerServiceController> _logger;
        private readonly IAzureFunctionCallerService _azureFunctionCaller;

        public AzureFunctionCallerController(ILogger<LoggerServiceController> logger, IAzureFunctionCallerService azureFunctionCaller)
        {
            _logger = logger;
            _azureFunctionCaller = azureFunctionCaller;
        }
        

        [HttpGet]
        [Route("GetDataByUser/{userId}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IEnumerable<string>> GetDataByUser(string userId)
        {
            IEnumerable<string> result = null;
            try
            {
                // Fake service for Azure Function Caller
                result = await _azureFunctionCaller.GetUserData(userId);

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
