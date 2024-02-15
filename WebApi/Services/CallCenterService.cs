using WebApi.Constants;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Services
{
    public class CallCenterService : ICallCenterService
    {
        public string ProcessCall(CallEvent callEvent)
        {
            if (callEvent == null)
            {
                throw new ArgumentNullException(nameof(callEvent), "Call event shouldn't be null");
            }

            

            var agentState = string.Empty; //tbd: should be the same as call action

            if (callEvent.Action == CallEventActions.CallStarted)
            {
                agentState = AgentStates.OnCall;
            }
            else if (callEvent.Action == CallEventActions.DoNotDisturb
            && callEvent.TimestampUtc.TimeOfDay >= TimeSpan.FromHours(11)
            && callEvent.TimestampUtc.TimeOfDay <= TimeSpan.FromHours(13))
            {
                agentState = AgentStates.OnLunch;
            }
            else if (DateTime.UtcNow.TimeOfDay - callEvent.TimestampUtc.TimeOfDay > TimeSpan.FromHours(1))
            {
                throw new LateEventException();
            }

            return agentState;
        }
    }
}