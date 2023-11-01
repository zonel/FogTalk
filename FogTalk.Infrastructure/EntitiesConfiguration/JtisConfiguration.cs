using FogTalk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FogTalk.Infrastructure.EntitiesConfiguration;


public class JtisConfiguration : IEntityTypeConfiguration<Jti>
{
    public void Configure(EntityTypeBuilder<Jti> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(j => j.JtiValue).IsRequired();
        builder.Property(j => j.BlacklistedAt).IsRequired();
    }
    
}