using FogTalk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FogTalk.Infrastructure.EntitiesConfiguration;

public class MessagesConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder
            .Property(m => m.Content)
            .IsRequired();
        
        builder
            .Property(m => m.Timestamp)
            .IsRequired();
        
        builder
            .Property(m => m.MessageStatus)
            .IsRequired();

        //Sender
        builder
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
            
        
        //Receiver
        builder
            .HasOne(m => m.ReceivingChat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId);
    }
}