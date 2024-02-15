using NUnit.Framework;

using WebApi.Constants;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests.Services
{
    [TestFixture]
    public class CallCenterServiceTests
    {
        private CallCenterService _callCenterService;

        [SetUp]
        public void Setup()
        {
            _callCenterService = new CallCenterService();
        }

        [Test]
        public void ProcessCall_ValidCallEvent_ReturnsAgentState()
        {
            // Arrange
            var callEvent = new CallEvent
            {
                Action = CallEventActions.CallStarted,
                TimestampUtc = DateTime.UtcNow
            };

            // Act
            var result = _callCenterService.ProcessCall(callEvent);

            // Assert
            Assert.AreEqual(AgentStates.OnCall, result);
        }

        [Test]
        public void ProcessCall_DoNotDisturbDuringLunchTime_ThrowsLateEventException()
        {
            // Arrange
            var callEvent = new CallEvent
            {
                Action = CallEventActions.DoNotDisturb,
                TimestampUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour - 2, 0, 0)
            };

            // Act and Assert
            Assert.Throws<LateEventException>(() => _callCenterService.ProcessCall(callEvent));
        }

        [Test]
        public void ProcessCall_NullCallEvent_ThrowsArgumentNullException()
        {
            // Arrange
            CallEvent callEvent = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _callCenterService.ProcessCall(callEvent));
        }

        [Test]
        public void ProcessCall_DoNotDisturbOutsideLunchTime_ReturnsAgentState()
        {
            // Arrange
            var callEvent = new CallEvent
            {
                Action = CallEventActions.DoNotDisturb,
                TimestampUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 12, 0, 0) // Set the timestamp outside lunchtime (2 PM)
            };

            // Act
            var result = _callCenterService.ProcessCall(callEvent);

            // Assert
            Assert.AreEqual(AgentStates.OnLunch, result);
        }
    }
}