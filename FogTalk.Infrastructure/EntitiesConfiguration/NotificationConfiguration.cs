using FogTalk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FogTalk.Infrastructure.EntitiesConfiguration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Message).IsRequired();
        //builder.Property(u => u.User).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.IsRead).IsRequired();
        
        //Owner
        // builder
        //     .HasOne(n => n.User)
        //     .WithMany(u => u.ReceivedNotifications)
        //     .HasForeignKey(n => n.UserId)
        //     .OnDelete(DeleteBehavior.Restrict);
    }
}