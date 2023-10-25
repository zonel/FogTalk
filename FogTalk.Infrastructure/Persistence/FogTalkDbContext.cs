using FogTalk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FogTalk.Infrastructure.Persistence;

public class FogTalkDbContext : DbContext
{
    public FogTalkDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}