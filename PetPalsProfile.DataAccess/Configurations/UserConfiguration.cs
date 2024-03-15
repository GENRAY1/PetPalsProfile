using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPalsProfile.DataAccess.Entities;
using PetPalsProfile.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsProfile.DataAccess.Configurations
{
    internal class UserConfiguration:IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.FirstName)
                .IsRequired();

            builder.Property(u => u.LastName)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Login)
                .HasMaxLength(User.MAX_LOGIN_LENGTH)
                .IsRequired();

            builder.Property(u => u.Password)
               .HasMaxLength(User.MAX_PASSWORD_LENGTH)
               .IsRequired();
        }
    }
}
