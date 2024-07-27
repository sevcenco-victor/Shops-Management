using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<ShopEntity>
    {
        public void Configure(EntityTypeBuilder<ShopEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(s => s.Image)
                     .IsRequired();

            builder.Property(s => s.Name)
                     .IsRequired()
                     .HasMaxLength(100);

            builder.Property(s => s.Address)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(s => s.Phone)
                     .IsRequired()
                     .HasMaxLength(31);

            builder.Property(s => s.Email)
                     .IsRequired()
                     .HasMaxLength(100);

            builder.Property(s => s.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Country)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
