using FogTalk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FogTalk.Infrastructure.EntitiesConfiguration;

public class ChatsConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder
            .Property(c => c.Name)
            .IsRequired();

        builder
            .Property(c => c.IsGroupChat)
            .IsRequired();
        
        //Participants
        builder
            .HasMany(c => c.Participants)
            .WithMany(u => u.Chats);
        
        //Messages
        builder
            .HasMany(c => c.Messages)
            .WithOne(m => m.ReceivingChat)
            .HasForeignKey(m => m.ChatId);
    }
    
}