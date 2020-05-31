using Ferroviario.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ferroviario.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<ChangeEntity> Changes { get; set; }

        public DbSet<RequestEntity> Requests { get; set; }

        public DbSet<RequestTypeEntity> RequestTypes { get; set; }

        public DbSet<ServiceDetailEntity> ServiceDetails { get; set; }

        public DbSet<ServiceEntity> Services { get; set; }

        public DbSet<ShiftEntity> Shifts { get; set; }

        public DbSet<ReportEntity> Reports { get; set; }

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

            builder.Entity<UserEntity>()
              .HasMany(u => u.Shifts)
              .WithOne(s => s.User);

            builder.Entity<ServiceEntity>()
             .HasMany(u => u.Shifts)
             .WithOne(s => s.Service);

            /*builder.Entity<UserEntity>()
              .HasMany(c => c.ChangesSent)
              .WithOne(c => c.FirstDriver);

            builder.Entity<UserEntity>()
              .HasMany(c => c.ChangesReceive)
              .WithOne(c => c.SecondDriver);*/
        }

        public DbSet<Ferroviario.Web.Data.Entities.ShiftEntity> ShiftEntity { get; set; }

    }
}
