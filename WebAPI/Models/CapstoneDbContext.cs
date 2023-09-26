
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{


    public partial class CapstoneDbContext : DbContext
    {
        public CapstoneDbContext()
        {
        }

        public CapstoneDbContext(DbContextOptions<CapstoneDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminInfo> AdminInfos { get; set; }

        public virtual DbSet<BlogInfo> BlogInfos { get; set; }

        public virtual DbSet<EmpInfo> EmpInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer(
                "server=DESKTOP-KQQHHP5\\SQLEXPRESS;database=CapstoneDB;trusted_connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AdminInf__3214EC078BC93F40");

                entity.ToTable("AdminInfo");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BlogInfo>(entity =>
            {
                entity.HasKey(e => e.BlogId).HasName("PK__BlogInfo__54379E306EE85E92");

                entity.ToTable("BlogInfo");

                entity.Property(e => e.BlogUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.DateOfCreation).HasColumnType("datetime");
                entity.Property(e => e.EmpEmailId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__EmpInfo__3214EC0723C3637F");

                entity.ToTable("EmpInfo");

                entity.HasIndex(e => e.EmailId, "UQ__EmpInfo__7ED91ACE0AFDD38E").IsUnique();

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");
                entity.Property(e => e.EmailId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}