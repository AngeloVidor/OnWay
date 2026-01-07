using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnWay.API.Domain.Entities;

namespace ONW_API.Infrastructure.Data
{
    public sealed class OnWayDbContext : DbContext
    {
        public DbSet<Transporter> Transporters => Set<Transporter>();

        public OnWayDbContext(DbContextOptions<OnWayDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OnWayDbContext).Assembly
            );
        }
    }
}