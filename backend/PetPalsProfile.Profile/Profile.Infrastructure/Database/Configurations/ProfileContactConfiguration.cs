using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Profiles;

namespace Profile.Infrastructure.Database.Configurations;

public class ProfileContactConfiguration : IEntityTypeConfiguration<ProfileContact>
{
    public void Configure(EntityTypeBuilder<ProfileContact> builder)
    {
        builder.HasKey(pc => new {pc.ProfileId, pc.ContactType});

        builder.Property(pc => pc.Link)
            .HasMaxLength(256);
    }
}