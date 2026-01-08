using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONW_API.Domain.Entities;
using OnWay.Domain.Transporters.ValueObjects;

namespace OnWay.Infrastructure.Configurations
{
    public sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(d => d.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(d => d.CreatedAt)
                   .IsRequired();

            builder.Property(d => d.TransporterId)
                   .IsRequired();

            builder.Property(d => d.VehicleId) 
                   .IsRequired();

            builder.OwnsOne(d => d.Phone, phone =>
            {
                phone.Property(p => p.Value)
                     .HasColumnName("Phone")
                     .IsRequired()
                     .HasMaxLength(20);
            });

            builder.HasOne<Vehicle>()
                   .WithMany()
                   .HasForeignKey(d => d.VehicleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
