using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Models;

public partial class InventoryContext : DbContext
{
    public InventoryContext()
    {
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FinancialYear> FinancialYears { get; set; }

    public virtual DbSet<MastBranch> MastBranches { get; set; }

    public virtual DbSet<MastComp> MastComps { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-P99NB78;Database=Inventory;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinancialYear>(entity =>
        {
            entity.HasKey(e => e.FinanYearId).HasName("PK_Financial Year");

            entity.ToTable("FinancialYear");
        });

        modelBuilder.Entity<MastBranch>(entity =>
        {
            entity.HasKey(e => e.BranchId);

            entity.ToTable("Mast_Branch");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Branch Name");
            entity.Property(e => e.BranchShortName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Branch Short Name");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Contact Person");
            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GstNo)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("GST No");
            entity.Property(e => e.MobileNo)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Mobile No");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Owner Name");
            entity.Property(e => e.PanNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAN No");
            entity.Property(e => e.PhoneNo)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Phone No");
            entity.Property(e => e.PinNo)
                .HasColumnType("numeric(7, 0)")
                .HasColumnName("PIN No");
            entity.Property(e => e.Website)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MastComp>(entity =>
        {
            entity.HasKey(e => e.CompId);

            entity.ToTable("Mast_Comp");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CompShortName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Comp. Short Name");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Company Name");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Contact Person");
            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GstNo)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("GST No");
            entity.Property(e => e.MobileNo)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Mobile No");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Owner Name");
            entity.Property(e => e.PanNo)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("PAN No");
            entity.Property(e => e.PhoneNo)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("Phone No");
            entity.Property(e => e.PinNo)
                .HasColumnType("numeric(7, 0)")
                .HasColumnName("PIN No");
            entity.Property(e => e.Website)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserMaster");

            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PersonName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Person Name");
            entity.Property(e => e.UpdationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Updation DateTime");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User Name");
            entity.Property(e => e.UserType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
