using Microsoft.EntityFrameworkCore;
using SampleTask.Models;
using System.Reflection.Metadata;

namespace SampleTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public  DbSet<City> Cities { get; set; }
        public  DbSet<Customer> Customers { get; set; }
        public  DbSet<CustomerCoworker> CustomerCoworkers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.CityId, "IX_Customer_CityId");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)                
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.City).WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_City");
            });

            modelBuilder.Entity<CustomerCoworker>(entity =>
            {
                entity.ToTable("CustomerCoworker");

                entity.Property(e => e.FkCoworkerId).HasColumnName("FK_CoworkerId");
                entity.Property(e => e.FkCustomerId).HasColumnName("FK_CustomerId");

                entity.HasOne(d => d.FkCoworker).WithMany(p => p.CustomerCoworkerFkCoworkers)
                    .HasForeignKey(d => d.FkCoworkerId)
                    .HasConstraintName("FK_CustomerCoworker_Coworker");

                entity.HasOne(d => d.FkCustomer).WithMany(p => p.CustomerCoworkerFkCustomers)
                    .HasForeignKey(d => d.FkCustomerId)
                    .HasConstraintName("FK_CustomerCoworker_Customer");
            });

        }
    }
   
}
