using Ferroviario.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ferroviario.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ChangeEntity> Changes { get; set; }

        public DbSet<RequestEntity> Requests { get; set; }

        public DbSet<RequestTypeEntity> RequestTypes { get; set; }

        public DbSet<ServiceDetailEntity> ServiceDetails { get; set; }

        public DbSet<ServiceEntity> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RequestTypeEntity>()
           .HasIndex(t => t.Type)
           .IsUnique();

            builder.Entity<ServiceEntity>()
           .HasIndex(t => t.Name)
           .IsUnique();

            builder.Entity<ServiceEntity>()
            .HasOne(a => a.ServiceDetail)
            .WithOne(a => a.Service)
            .HasForeignKey<ServiceDetailEntity>(c => c.Id);


        }

    }
}
