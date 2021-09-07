using System.Threading.Tasks;
using GateWayApi.DAL.Entity;
using GateWayApi.DAL.Repository;
using GateWayApi.Services.AzureFunCaller;
using GateWayApi.Services.LoggerRepo;
using GateWayApi.Services.Services;
using Moq;
using Xunit;

namespace GateWay.WebApi.Test.Services
{
   public class LoggerServiceTests
    {

        [Fact(DisplayName = "Run LoggerService Verify Repository Function is Hit")]
        public async Task Run_LoggerServiceTests_Verify_RepositoryFunction_Hit()
        {
            // Arrange
            var logItem=new LogItem();
            var logRepository = new Mock<IGenericRepository<LogItem>>();
            logRepository.Setup(r => r.Insert(logItem));
            var loggerService = new LoggerService(logRepository.Object);

            // Act
             loggerService.AddToLogs(logItem);
            
            // Assert
            logRepository.Verify(x => x.Insert(logItem), Times.Exactly(1));
        }
    }
}
