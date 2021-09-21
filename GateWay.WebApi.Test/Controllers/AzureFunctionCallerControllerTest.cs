using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using GateWayApi.DAL.Controllers;
using GateWayApi.Shared.AzureFunCaller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;
using FluentAssertions.Types;
using GateWayApi.DAL;
using Microsoft.AspNetCore.Hosting;

namespace GateWay.WebApi.Test.Controllers
{

    public class AzureFunctionCallerControllerTest
    {
        private readonly Mock<ILogger<LoggerServiceController>> _logger;
        private readonly Mock<IAzureFunctionCallerService> _azureFunctionCaller;

        private async Task<IEnumerable<string>> GetUserData()
        {
            var list = new List<string> { $"test data ", $"test data 01" };
            return (IEnumerable<string>) list;
        }

        [Fact(DisplayName = "Run Azure Function return array of string")]
        public async Task Run_Azure_Function_Return_Array_Of_String()
        {
            // Arrange
            var logger = new Mock<ILogger<LoggerServiceController>>();
            var azureFunctionCaller = new Mock<IAzureFunctionCallerService>();

            azureFunctionCaller.Setup(f => f.GetUserData("test")).Returns(GetUserData());
            var controller = new AzureFunctionCallerController(logger.Object, azureFunctionCaller.Object);

            // Act
            var result = await controller.GetDataByUser("test");

            // Assert
            var list = result.GetEnumerator();
            Assert.Equal(2,result.Count());
        }

        [Fact(DisplayName = "Run Azure Function Verify that GetUserData is called once ")]
        public async Task Run_Azure_Function_Verify_that_GetUserData_is_called_once()
        {
            // Arrange
            var logger = new Mock<ILogger<LoggerServiceController>>();
            var azureFunctionCaller = new Mock<IAzureFunctionCallerService>();

            azureFunctionCaller.Setup(f => f.GetUserData("test")).Returns(GetUserData());
            var controller = new AzureFunctionCallerController(logger.Object, azureFunctionCaller.Object);

            // Act
            var result = await controller.GetDataByUser("test");

            // Assert
            azureFunctionCaller.Verify(x => x.GetUserData(It.IsAny<string>()), Times.Exactly(1));
        }


        [Fact (DisplayName = "Get Data By User Returns list ")]
        public async Task GetDataByUser_ReturnsAStringArray()
        {
            // Arrange
            var logger = new Mock<ILogger<LoggerServiceController>>();
            var azureFunctionCaller = new Mock<IAzureFunctionCallerService>();

            azureFunctionCaller.Setup(f => f.GetUserData("test")).Returns(GetUserData());
            var controller = new AzureFunctionCallerController(logger.Object, azureFunctionCaller.Object);

            // Act
            var result = await controller.GetDataByUser("test");
            
            // Assert
            Assert.IsType<System.Collections.Generic.List<string>>(result);
        }



        [Fact(DisplayName = "Validate LoggerServiceController must have Authorization Attribute")]
        public void LoggerServiceController_Should_Implement_Authorize_Attribute()
        {
            var excludedTypes = new[]
            {
                typeof(LoggerServiceController),
                typeof(UsersController)
            };
            var assembly = Assembly.GetAssembly(typeof(AzureFunctionCallerController));
            var allControllers = AllTypes.From(assembly).ThatDeriveFrom<ControllerBase>().Except(excludedTypes);

            var controllersWithoutAuthorizeAttribute = allControllers.Where(t => !t.IsDefined(typeof(AuthorizeAttribute), false)).ToList();
            var controllersName = string.Join(" and ", controllersWithoutAuthorizeAttribute.Select(x => x.Name));

            controllersWithoutAuthorizeAttribute.Count.Should().Be(1, "because {0} should have the Authorize attribute", controllersName);
            Assert.Equal("AzureFunctionCallerController",controllersName);
        }
        
    }
}




