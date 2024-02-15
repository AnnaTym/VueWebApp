using WebApi.Models;

namespace WebApi.Services
{
    public interface IAgentRepository
    {
         void UpdateInsertAgent(Agent agent);
         IEnumerable<Agent> GetAllAgents();
    }
}