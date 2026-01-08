using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONW_API.Domain.Entities;

namespace OnWay.Infrastructure.Configurations
{
    public sealed class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("Shipments");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.TransporterId).IsRequired();
            builder.Property(s => s.VehicleId).IsRequired();
            builder.Property(s => s.DriverId);
            builder.Property(s => s.CreatedAt).IsRequired();

            builder.Property(s => s.Status)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);

            builder.OwnsOne(s => s.Origin, origin =>
            {
                origin.Property(o => o.Address).HasColumnName("OriginAddress").IsRequired().HasMaxLength(200);
                origin.Property(o => o.City).HasColumnName("OriginCity").IsRequired().HasMaxLength(100);
                origin.Property(o => o.State).HasColumnName("OriginState").IsRequired().HasMaxLength(50);
                origin.Property(o => o.Latitude).HasColumnName("OriginLat").IsRequired();
                origin.Property(o => o.Longitude).HasColumnName("OriginLng").IsRequired();
            });

            builder.OwnsOne(s => s.Destination, destination =>
            {
                destination.Property(d => d.Address).HasColumnName("DestinationAddress").IsRequired().HasMaxLength(200);
                destination.Property(d => d.City).HasColumnName("DestinationCity").IsRequired().HasMaxLength(100);
                destination.Property(d => d.State).HasColumnName("DestinationState").IsRequired().HasMaxLength(50);
                destination.Property(d => d.Latitude).HasColumnName("DestinationLat").IsRequired();
                destination.Property(d => d.Longitude).HasColumnName("DestinationLng").IsRequired();
            });

            builder.HasMany(s => s.Packages)
                   .WithOne()
                   .HasForeignKey("ShipmentId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
