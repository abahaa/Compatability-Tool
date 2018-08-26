using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompatabilityLiberaryCore1.Models
{
    public partial class ComptabilityContext : DbContext
    {
        private  string connectionstring;
        public ComptabilityContext(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public ComptabilityContext(DbContextOptions<ComptabilityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Components> Components { get; set; }
        public virtual DbSet<ComptabilityMatrix> ComptabilityMatrix { get; set; }
        public virtual DbSet<Releases> Releases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Components>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ComptabilityMatrix>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DependantComponentId).HasColumnName("dependantComponentID");

                entity.Property(e => e.MasterComponentId).HasColumnName("masterComponentID");

                entity.Property(e => e.MasterVersion).HasColumnName("masterVersion");

                entity.Property(e => e.MaxDependVersion).HasColumnName("maxDependVersion");

                entity.Property(e => e.MinDependVersion).HasColumnName("minDependVersion");
            });

            modelBuilder.Entity<Releases>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.ReleaseNo).HasColumnName("ReleaseNO");
            });
        }
    }
}
