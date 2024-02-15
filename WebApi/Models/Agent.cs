
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Agent
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("state")]
        public string State { get; set; }
        [Column("skills")]
        public List<Guid> Skills { get; set; }
    }
}