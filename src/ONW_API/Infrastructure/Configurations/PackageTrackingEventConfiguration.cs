using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONW_API.Domain.Entities;
using ONW_API.Domain.ValueObjects;

namespace OnWay.Infrastructure.Configurations
{
    public sealed class PackageTrackingEventConfiguration : IEntityTypeConfiguration<PackageTrackingEvent>
    {
        public void Configure(EntityTypeBuilder<PackageTrackingEvent> builder)
        {
            builder.ToTable("PackageTrackingEvents");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(e => e.DriverId);

            builder.OwnsOne(e => e.Location, loc =>
            {
                loc.Property(l => l.Address)
                .HasColumnName("LocationAddress")
                .HasMaxLength(200);

                loc.Property(l => l.City)
                .HasColumnName("LocationCity")
                .HasMaxLength(100);

                loc.Property(l => l.State)
                .HasColumnName("LocationState")
                .HasMaxLength(50);
            });


        }
    }
}
