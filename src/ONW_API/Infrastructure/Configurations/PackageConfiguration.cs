using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONW_API.Domain.Entities;

namespace OnWay.Infrastructure.Configurations
{
    public sealed class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Packages");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TrackingCode)
                   .IsRequired();

            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.ShipmentId)
                   .IsRequired();

            builder.OwnsOne(p => p.Recipient, recipient =>
            {
                recipient.Property(r => r.Name)
                         .HasColumnName("RecipientName")
                         .IsRequired()
                         .HasMaxLength(200);

                recipient.OwnsOne(r => r.Email, e =>
                {
                    e.Property(x => x.Value)
                     .HasColumnName("RecipientEmail")
                     .IsRequired()
                     .HasMaxLength(200);
                });

                recipient.OwnsOne(r => r.Phone, ph =>
                {
                    ph.Property(x => x.Value)
                      .HasColumnName("RecipientPhone")
                      .IsRequired()
                      .HasMaxLength(20);
                });

                recipient.OwnsOne(r => r.Address, addr =>
                {
                    addr.Property(a => a.Street).HasColumnName("RecipientStreet").IsRequired().HasMaxLength(200);
                    addr.Property(a => a.Number).HasColumnName("RecipientNumber").IsRequired().HasMaxLength(20);
                    addr.Property(a => a.District).HasColumnName("RecipientDistrict").IsRequired().HasMaxLength(100);
                    addr.Property(a => a.City).HasColumnName("RecipientCity").IsRequired().HasMaxLength(100);
                    addr.Property(a => a.State).HasColumnName("RecipientState").IsRequired().HasMaxLength(50);
                    addr.Property(a => a.ZipCode).HasColumnName("RecipientZipCode").IsRequired().HasMaxLength(20);
                });
            });

            builder.HasMany(p => p.TrackingEvents)
                   .WithOne()
                   .HasForeignKey("PackageId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
