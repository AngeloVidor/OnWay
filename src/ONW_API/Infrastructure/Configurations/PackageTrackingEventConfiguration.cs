// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using ONW_API.Domain.Entities;

// namespace OnWay.Infrastructure.Configurations
// {
//     public sealed class PackageTrackingEventConfiguration : IEntityTypeConfiguration<PackageTrackingEvent>
//     {
//         public void Configure(EntityTypeBuilder<PackageTrackingEvent> builder)
//         {
//             builder.ToTable("PackageTrackingEvents");
//             builder.HasKey(e => e.Id);

//             builder.Property(e => e.OccurredAt).IsRequired();
//             builder.Property(e => e.Location).IsRequired().HasMaxLength(200);
//             builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
//         }
//     }
// }
