using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChartsServer.Models;

public partial class ChartsDbContext : DbContext
{
    public ChartsDbContext()
    {
    }

    public ChartsDbContext(DbContextOptions<ChartsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Personel> Personels { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KTE9MEP;Database=ChartsDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Personel>(entity =>
        {
            entity.Property(e => e.PersonelId).HasColumnName("PersonelID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesId);

            entity.Property(e => e.SalesId).HasColumnName("SalesID");
            entity.Property(e => e.PersonelId).HasColumnName("PersonelID");

            entity.HasOne(d => d.Personel).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PersonelId)
                .HasConstraintName("FK_Sales_Personels");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
