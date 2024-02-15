using Microsoft.AspNetCore.Mvc;

using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/callcenter")]
    public class CallCenterController : ControllerBase
    {
        private readonly ICallCenterService _callCenterService;
        private readonly IAgentRepository _agentService;

        public CallCenterController(
          ICallCenterService callCenterService,
          IAgentRepository agentService)
        {
            _callCenterService = callCenterService;
            _agentService = agentService;
        }

        [HttpPost]
        public IActionResult ProcessEvent(CallEvent callEvent)
        {
            try
            {
                // Business logic to determine the agent's state
                var agentState = _callCenterService.ProcessCall(callEvent);

                var agent = new Agent
                {
                    Id = callEvent.AgentId,
                    Name = callEvent.AgentName,
                    State = agentState,
                    Skills = callEvent.QueueIds
                };

                // Update the agent's state and skills in the database
                _agentService.UpdateInsertAgent(agent);
                return Ok(agent);
            }
            catch (LateEventException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAgents()
        {
            try
            {
                var agents = _agentService.GetAllAgents();
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}