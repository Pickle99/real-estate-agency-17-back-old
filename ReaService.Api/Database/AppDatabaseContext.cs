using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using real_estate_agency_17_back.ReaService.Api.Models;
using System.Collections.Generic;

namespace PostgreSQL.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary Key

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Auto-increment

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(35);

                entity.HasIndex(e => e.Username)
                      .IsUnique(); // Unique Index on Username

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(45);

                entity.HasIndex(e => e.Email)
                      .IsUnique(); // Unique Index on Email

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(32); // Max Length for Password

                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(25);

                // Configure CreatedAt and UpdatedAt
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Set default value for creation

                entity.Property(e => e.UpdatedAt)
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP")
                      .IsConcurrencyToken(); // Update on every change
            });
        }

        public override int SaveChanges()
        {
            // Automatically set UpdatedAt on updates
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
