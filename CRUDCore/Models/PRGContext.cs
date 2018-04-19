using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDCore.Models
{
    public partial class PRGContext : DbContext
    {
        public virtual DbSet<Citizen> Citizen { get; set; }

        public PRGContext(DbContextOptions<PRGContext> options)
            : base(options)
        {}

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer(@"Data Source=REYMER-PC;Initial Catalog=PRG;Integrated Security=True");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>(entity =>
            {
                entity.HasIndex(e => e.CitizenIdentification)
                    .HasName("UQ__Citizen__3C297E771B2CA3A3")
                    .IsUnique();

                entity.Property(e => e.CitizenId).HasColumnName("CitizenID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecondName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
