using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
               .Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder
               .Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(100);

            builder
                .Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
