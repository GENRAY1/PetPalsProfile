using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Infrastructure.Database.Configurations;

public class PetTypeConfiguration : IEntityTypeConfiguration<PetType>
{
    public void Configure(EntityTypeBuilder<PetType> builder)
    {
        builder.HasKey(petType => petType.Id);

        builder.Property(petType => petType.Name)
            .HasMaxLength(64);
    }
}