using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CrawlerDemo
{
    public partial class carinforContext : DbContext
    {
        public carinforContext()
        {
        }

        public carinforContext(DbContextOptions<carinforContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carinfor1> Carinfor1s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=desktop-eprhhtb\\sqlexpress; Initial Catalog=carinfor; User Id= Bogdan1; Password=bobo88214828bibi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carinfor1>(entity =>
            {
                entity.HasKey(x => x.ID);

                entity.ToTable("carinfor1");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)

                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
