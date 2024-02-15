using WebApi.Context;
using WebApi.Models;

namespace WebApi.Services
{
    public class PostgreSqlAgentService : IAgentRepository
    {
        private readonly AgentDbContext _dbContext;

        public PostgreSqlAgentService(AgentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Agent> GetAllAgents()
        {
            return _dbContext.Agents.ToList();
        }

        public void UpdateInsertAgent(Agent agent)
        {
            Agent dbAgent = _dbContext.Agents.FirstOrDefault(a => a.Id == agent.Id);
            if (dbAgent != null)
            {
                dbAgent.State = agent.State;
                dbAgent.Skills = agent.Skills;
            }
            else
            {
                _dbContext.Agents.Add(agent);
            }

            _dbContext.SaveChanges();
        }
    }
}