using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GateWayApi.DAL.Controllers;
using GateWayApi.DAL.Repository;
using GateWayApi.Services.AzureFunCaller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;


namespace GateWay.WebApi.Test.Controllers
{

    public class AzureFunctionCallerControllerTest
    {
        private readonly Mock<ILogger<LoggerServiceController>> _logger;
        private readonly Mock<IAzureFunctionCallerService> _azureFunctionCaller;

        public AzureFunctionCallerControllerTest(ILogger<LoggerServiceController> logger,
            IAzureFunctionCallerService azureFunctionCaller)
        {
            _logger = new Mock<ILogger<LoggerServiceController>>();
            
            _azureFunctionCaller = new Mock<IAzureFunctionCallerService>();
            

        }

        //[Fact]
        //public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IGenericRepository<>>();
        //    mockRepo.Setup(repo => repo.ListAsync())
        //        .ReturnsAsync(GetTestSessions());
        //    var controller = new HomeController(mockRepo.Object);

        //    // Act
        //    var result = await controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
        //        viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        //[Theory(DisplayName = "Call_GetAsync_Method_Should_Return_ArgumentNullException")]
        //[InlineData("")]
        //public void Call_GetAsync_Method_Should_Return_ArgumentNullException(string dataProvide)
        //{
        //    var cacher = new Mock<ITimelyCache<dynamic>>();
        //    //Arrange
        //    var service = new StructureCommonDataService(cacher.Object);
        //    var controller = new StructureCommonDataController(service); // WebAPI controller

        //    //ACT
        //    var exception = Record.ExceptionAsync(async () => await controller.GetAsync(dataProvide));

        //    // Assert 
        //    Assert.ThrowsAsync<ArgumentNullException>(async () => await controller.GetAsync(dataProvide));

        //}
    }
}




