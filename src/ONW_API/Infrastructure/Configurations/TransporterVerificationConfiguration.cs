using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONW_API.Domain.Entities;
using OnWay.Domain.Transporters.ValueObjects;

namespace OnWay.Infrastructure.Configurations;

public sealed class TransporterVerificationConfiguration
    : IEntityTypeConfiguration<TransporterVerification>
{
    public void Configure(EntityTypeBuilder<TransporterVerification> builder)
    {
        builder.ToTable("TransporterVerifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(6); 

        builder.Property(x => x.ExpiresAt)
            .IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(150);

            email.HasIndex(e => e.Value)
                .IsUnique();
        });

        builder.OwnsOne(x => x.Phone, phone =>
        {
            phone.Property(p => p.Value)
                .HasColumnName("Phone")
                .IsRequired()
                .HasMaxLength(20);
        });

        builder.OwnsOne(x => x.Password, password =>
        {
            password.Property(p => p.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();
        });

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
