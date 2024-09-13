using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profile.Infrastructure.Database.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Domain.Profiles.Profile>
{
    public void Configure(EntityTypeBuilder<Domain.Profiles.Profile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .HasMaxLength(600);

        builder.Property(p => p.Username)
            .HasMaxLength(32);

        builder.Property(p => p.MainPhoto)
            .HasMaxLength(256);
        
        builder.HasMany(p => p.Contacts)
            .WithOne()
            .HasForeignKey(pc => pc.ProfileId);
    }
}