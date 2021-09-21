using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GateWayApi.DAL.Entity;
using GateWayApi.DAL.Repository;
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




   public interface IAlertDAO
   {
       Guid AddAlert(DateTime time);

       DateTime GetAlert(Guid id);
   }

   public class AlertService
   {
       private readonly IAlertDAO storage;

       public AlertService(IAlertDAO alertDAO)
       {
           storage = alertDAO;
       }

       public Guid RaiseAlert(IAlertDAO storage)
       {
           return this.storage.AddAlert(DateTime.Now);
       }

       public DateTime GetAlertTime(Guid id)
       {
           return storage.GetAlert(id);
       }
   }

   public class AlertDAO : IAlertDAO
   {
       private readonly Dictionary<Guid, DateTime> alerts = new Dictionary<Guid, DateTime>();


       public Guid AddAlert(DateTime time)
       {
           Guid id = Guid.NewGuid();
           this.alerts.Add(id, time);
           return id;
       }

       public DateTime GetAlert(Guid id)
       {
           return this.alerts[id];
       }
   }
}
