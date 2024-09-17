using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;

namespace PetPalsProfile.Infrastructure.Database.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Phone)
            .HasMaxLength(Account.MaxPhoneLength);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(Account.MaxEmailLength);
        
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        
        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt);
        
        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);
        
        builder.OwnsOne(u => u.RefreshToken);
    }
}