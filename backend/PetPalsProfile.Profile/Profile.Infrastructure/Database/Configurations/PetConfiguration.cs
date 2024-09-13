using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Infrastructure.Database.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(pet => pet.Id);

        builder.Property(pet => pet.Description)
            .HasMaxLength(600);
        
        builder.Property(p => p.MainPhoto)
            .HasMaxLength(256);

        builder.Property(p => p.Name)
            .HasMaxLength(64);

        builder.HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(el => el.TypeId);
        
        builder.HasOne<Domain.Profiles.Profile>()
            .WithMany()
            .HasForeignKey(pet => pet.ProfileId);
    }
}