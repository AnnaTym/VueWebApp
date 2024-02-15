using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using WebApi.Constants;
using WebApi.Controllers;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests.Controllers
{
    [TestFixture]
    public class CallCenterControllerTests
    {
        private Mock<ICallCenterService> _callCenterServiceMock;
        private Mock<IAgentRepository> _agentServiceMock;
        private CallCenterController _callCenterController;

        [SetUp]
        public void Setup()
        {
            _callCenterServiceMock = new Mock<ICallCenterService>();
            _agentServiceMock = new Mock<IAgentRepository>();
            _callCenterController = new CallCenterController(_callCenterServiceMock.Object, _agentServiceMock.Object);
        }

        [Test]
        public void ProcessEvent_ValidCallEvent_ReturnsOk()
        {
            // Arrange
            var callEvent = new CallEvent
            {
                AgentId = Guid.NewGuid(),
                AgentName = "John Doe",
                QueueIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };
            var agentState = AgentStates.OnCall;
            var expectedAgent = new Agent
            {
                Id = callEvent.AgentId,
                Name = callEvent.AgentName,
                State = agentState,
                Skills = callEvent.QueueIds
            };

            _callCenterServiceMock.Setup(m => m.ProcessCall(callEvent)).Returns(agentState);
            _agentServiceMock.Setup(m => m.UpdateInsertAgent(expectedAgent));

            // Act
            var result = _callCenterController.ProcessEvent(callEvent) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedAgent.Id, (result.Value as Agent).Id);
            Assert.AreEqual(expectedAgent.Name, (result.Value as Agent).Name);
            Assert.AreEqual(expectedAgent.State, (result.Value as Agent).State);
            Assert.AreEqual(expectedAgent.Skills, (result.Value as Agent).Skills);
        }

        [Test]
        public void ProcessEvent_LateEventException_ReturnsBadRequest()
        {
            // Arrange
            var callEvent = new CallEvent();
            _callCenterServiceMock.Setup(m => m.ProcessCall(callEvent)).Throws(new LateEventException());

            // Act
            var result = _callCenterController.ProcessEvent(callEvent) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void ProcessEvent_GenericException_ReturnsInternalServerError()
        {
            // Arrange
            var callEvent = new CallEvent();
            var exceptionMessage = "An error occurred.";
            _callCenterServiceMock.Setup(m => m.ProcessCall(callEvent)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _callCenterController.ProcessEvent(callEvent) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual(exceptionMessage, result.Value);
        }

        [Test]
        public void GetAgents_ReturnsOk()
        {
            // Arrange
            var agents = new[] { new Agent(), new Agent() };
            _agentServiceMock.Setup(m => m.GetAllAgents()).Returns(agents);

            // Act
            var result = _callCenterController.GetAgents() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(agents, result.Value);
        }

        [Test]
        public void GetAgents_GenericException_ReturnsInternalServerError()
        {
            // Arrange
            var exceptionMessage = "An error occurred.";
            _agentServiceMock.Setup(m => m.GetAllAgents()).Throws(new Exception(exceptionMessage));

            // Act
            var result = _callCenterController.GetAgents() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual(exceptionMessage, result.Value);
        }
    }
}