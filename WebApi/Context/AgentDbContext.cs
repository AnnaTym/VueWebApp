using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Context
{
    public class AgentDbContext : DbContext
    {
        public AgentDbContext(DbContextOptions<AgentDbContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>()
            .ToTable("agent")
            .HasKey(a => a.Id);

            // Map properties to columns
            modelBuilder.Entity<Agent>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Agent>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Agent>()
                .Property(a => a.State)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}