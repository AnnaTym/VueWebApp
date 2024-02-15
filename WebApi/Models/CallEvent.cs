namespace WebApi.Models
{
    public class CallEvent
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime TimestampUtc { get; set; }
        public string Action { get; set; }
        public List<Guid> QueueIds { get; set; }
    }
}