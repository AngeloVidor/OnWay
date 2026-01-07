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
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransporterId).IsRequired();
            builder.Property(x => x.PickupDate).IsRequired();
            builder.Property(x => x.EstimatedDeliveryDate).IsRequired();
            builder.Property(x => x.Notes).HasMaxLength(500);

            builder.OwnsOne(x => x.Origin, origin =>
            {
                origin.Property(o => o.Address).HasColumnName("OriginAddress").IsRequired().HasMaxLength(200);
                origin.Property(o => o.City).HasColumnName("OriginCity").IsRequired().HasMaxLength(100);
                origin.Property(o => o.State).HasColumnName("OriginState").IsRequired().HasMaxLength(50);
            });

            builder.OwnsOne(x => x.Destination, destination =>
            {
                destination.Property(d => d.Address).HasColumnName("DestinationAddress").IsRequired().HasMaxLength(200);
                destination.Property(d => d.City).HasColumnName("DestinationCity").IsRequired().HasMaxLength(100);
                destination.Property(d => d.State).HasColumnName("DestinationState").IsRequired().HasMaxLength(50);
            });

            builder.OwnsMany(x => x.Products, product =>
            {
                product.WithOwner().HasForeignKey("ShipmentId");
                product.Property<int>("Id");
                product.HasKey("Id");
                product.Property(p => p.Name).IsRequired().HasMaxLength(150);
                product.Property(p => p.Quantity).IsRequired();
                product.Property(p => p.Weight).IsRequired();
            });

            builder.Property(x => x.DriverId);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }

    public sealed class ShipmentTrackingEventConfiguration : IEntityTypeConfiguration<ShipmentTrackingEvent>
    {
        public void Configure(EntityTypeBuilder<ShipmentTrackingEvent> builder)
        {
            builder.ToTable("ShipmentTrackingEvents");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.Location).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(500);

            builder.HasOne<Shipment>()
                   .WithMany(s => s.TrackingEvents)
                   .HasForeignKey(t => t.ShipmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
