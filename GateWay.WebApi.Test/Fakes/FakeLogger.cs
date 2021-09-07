using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GateWay.WebApi.Test.Fakes
{
    public class FakeLogger<T> : Mock<ILogger>
    {
        private List<(LogLevel level, object item)> _events;
        private readonly ILogger<T> _logger;
        public ILogger<T> Instance => _logger;

        public FakeLogger()
        {
            _events = new List<(LogLevel level, object item)>();

            var log = new Mock<ILogger<T>>();

            var events = new List<(LogLevel level, object item)>();

            log.Setup(l => l.Log(It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>())

            ).Callback(new InvocationAction(invocation =>
            {
                _events.Add(((LogLevel)invocation.Arguments[0], invocation.Arguments[2]));
            }));

            _logger = log.Object;
        }

        public void Verify(LogLevel level)
        {
            if (!_events.Any(e => e.level == level))
            {
                throw new Exception($"Coudln't find any log level of '{level}' out of {_events.Count()} logs.");
            }
        }

        public void Verify(LogLevel level, string containsMessage)
        {
            if (!_events.Any(e => e.level == level && e.item.ToString().Contains(containsMessage)))
            {
                throw new Exception($"Coudln't find any log level of '{level}' containing the message '{containsMessage}' out of {_events.Count()} logs.");
            }
        }
    }
}
