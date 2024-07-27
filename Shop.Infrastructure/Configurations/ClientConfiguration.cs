using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);      
            
            builder
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100); 

            builder
                .Property(c=> c.Gender) 
                .IsRequired()
                .HasMaxLength(10);

            builder
                .Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100); 

            builder
                .Property(c => c.City)
                .IsRequired()
                .HasMaxLength(100); 

            builder
                .Property(c => c.Country)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
