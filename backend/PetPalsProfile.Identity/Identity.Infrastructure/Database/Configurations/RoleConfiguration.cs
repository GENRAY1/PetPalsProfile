using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPalsProfile.Domain.UserAccounts;

namespace PetPalsProfile.Infrastructure.Database.Configurations;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .HasMaxLength(Role.MaxNameLength);
        
        builder.Property(r => r.Localization)
            .HasMaxLength(Role.MaxLocalizationLength);

        builder.HasData(Role.Roles);
    }
}