using MongoDB.Driver;
using WebApi.Models;

namespace WebApi.Services
{
    public class MongoDbAgentService : IAgentRepository
    {
        private readonly IMongoCollection<Agent> _agentsCollection;

        public MongoDbAgentService(IMongoDatabase database)
        {
            _agentsCollection = database.GetCollection<Agent>("Agent");
        }

        public IEnumerable<Agent> GetAllAgents()
        {
            return _agentsCollection.Find(_ => true).ToList();
        }

        public void UpdateInsertAgent(Agent agent)
        {
            var filter = Builders<Agent>.Filter.Eq(a => a.Id, agent.Id);
            var update = Builders<Agent>.Update
            .Set(a => a.State, agent.State)
            .Set(a => a.Skills, agent.Skills);
            _agentsCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }
}