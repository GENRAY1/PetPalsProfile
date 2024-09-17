using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Subscription;

namespace Profile.Infrastructure.Database.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(sub => sub.Id);

        builder.HasOne<Domain.Profiles.Profile>()
            .WithMany()
            .HasForeignKey(sub => sub.SubscribedToId);
        
        builder.HasOne<Domain.Profiles.Profile>()
            .WithMany()
            .HasForeignKey(sub => sub.SubscriberId);
    }
}