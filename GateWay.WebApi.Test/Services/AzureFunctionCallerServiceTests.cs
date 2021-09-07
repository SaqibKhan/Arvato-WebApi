using System.Linq;
using System.Threading.Tasks;
using GateWayApi.DAL.Controllers;
using GateWayApi.Services.AzureFunCaller;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GateWay.WebApi.Test.Services
{
   public class AzureFunctionCallerServiceTests
    {
        [Fact(DisplayName = "Run AzureFunction Caller Service VerifyResult list type")]
        public async Task Run_AzureFunctionCallerService_VerifyResult()
        {
            // Arrange
           var azureFunctionCaller =new AzureFunctionCallerService();

            // Act
            var result = await azureFunctionCaller.GetUserData("test");

            // Assert
            // Assert
            Assert.IsType<System.Collections.Generic.List<string>>(result);
        }
    }
}
