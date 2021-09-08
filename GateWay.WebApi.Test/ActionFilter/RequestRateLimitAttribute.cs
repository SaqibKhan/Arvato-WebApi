using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GateWayApi.DAL.ActionFilter;
using GateWayApi.DAL.Entity;
using GateWayApi.DAL.Repository;
using GateWayApi.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace GateWay.WebApi.Test.ActionFilter
{
   public class RequestRateLimitAttributeTests
    {
        [Theory(DisplayName = "Run validation Filter Check the Result and Forbidden")]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 0)]
        [InlineData(2, 1, 1)]
        public void Run_validationFilter_check_hit(int request, int seconds, int delay)
        {
            // Act
            if (request == 1)
            {
                Run_OneTime_validationFilter_check_hit(seconds);  // single call
            }
            else if (request > 1 && delay < 0)
            {
                Run_Consectively_ValidationFilter_check_hit(seconds); // double call with no delay
            }
            else if (request > 1 && delay >seconds)
            {
                Run_Consectively_ValidationFilter_check_hit(seconds); // double call with delay 
            }

        }


        private void Run_OneTime_validationFilter_check_hit(int seconds)
        {
            // Arrange
            var validationFilter = new RequestRateLimitAttribute { Name = "test", Seconds = seconds };
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse("192.168.0.27");
            var context = new ActionContext(httpContext, Mock.Of<RouteData>(), Mock.Of<ActionDescriptor>(), modelState);
            var actionExecutingContext = new ActionExecutedContext(context, new List<IFilterMetadata>(), Mock.Of<ControllerBase>());

            // Act
            validationFilter.OnActionExecuted(actionExecutingContext);

            // Assert
            var scheme = context.HttpContext.Request.Scheme;
            var content = (ContentResult)actionExecutingContext.Result;
            
            Assert.Empty(scheme);
            Assert.Null(content);
        }


        //[Fact(DisplayName = "Send Two request in one second and check Forbidden status")]
        private  void Run_Consectively_ValidationFilter_check_hit(int seconds)
        {
            // Arrange
            var validationFilter = new RequestRateLimitAttribute { Name = "test", Seconds = seconds };
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
           var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse("192.168.0.27");
            var context = new ActionContext(httpContext, Mock.Of<RouteData>(), Mock.Of<ActionDescriptor>(), modelState);


            var actionExecutingContext = new ActionExecutedContext(context, new List<IFilterMetadata>(), Mock.Of<ControllerBase>());

            // Act
            validationFilter.OnActionExecuted(actionExecutingContext);
            validationFilter.OnActionExecuted(actionExecutingContext);

            // Assert
            var scheme = context.HttpContext.Request.Scheme;
            var content = (ContentResult)actionExecutingContext.Result;
            Assert.Equal(StatusCodes.Status429TooManyRequests.ToString(), scheme);
            Assert.NotNull(content);
        }

        private void Run_MoreTheOneTime_WithDelay_validated_Status429TooManyRequests(int seconds,int delay)
        {
            // Arrange
            var validationFilter = new RequestRateLimitAttribute { Name = "test", Seconds = seconds };
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse("192.168.0.27");
            var context = new ActionContext(httpContext, Mock.Of<RouteData>(), Mock.Of<ActionDescriptor>(), modelState);
            var actionExecutingContext = new ActionExecutedContext(context, new List<IFilterMetadata>(), Mock.Of<ControllerBase>());

            // Act
            validationFilter.OnActionExecuted(actionExecutingContext);
            Thread.Sleep(new TimeSpan(0,0,0,seconds));
            validationFilter.OnActionExecuted(actionExecutingContext);

            // Assert
            var scheme = context.HttpContext.Request.Scheme;
            var content = (ContentResult)actionExecutingContext.Result;

            Assert.Empty(scheme);
            Assert.Null(content);
        }
    }

   
}
