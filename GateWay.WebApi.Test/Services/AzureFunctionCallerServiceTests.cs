using System.Threading.Tasks;
using GateWayApi.Services;
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
