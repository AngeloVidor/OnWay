using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONW_API.Domain.Entities;

namespace OnWay.Infrastructure.Configurations
{
       public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
       {
              public void Configure(EntityTypeBuilder<Vehicle> builder)
              {
                     builder.ToTable("Vehicles");
                     builder.HasKey(v => v.Id);

                     builder.Property(v => v.Plate)
                            .IsRequired()
                            .HasMaxLength(20);

                     builder.Property(v => v.Model)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(v => v.TransporterId)
                            .IsRequired();

                     builder.Property(v => v.CreatedAt)
                            .IsRequired();

                     builder.Property(v => v.Status)
                            .IsRequired()
                            .HasConversion<string>();
              }
       }
}
