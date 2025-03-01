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

    public virtual DbSet<ItemCatagory> ItemCatagories { get; set; }

    public virtual DbSet<ItemCompany> ItemCompanies { get; set; }

    public virtual DbSet<ItemSubCatagory> ItemSubCatagories { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<ItemUnit> ItemUnits { get; set; }

    public virtual DbSet<MastBranch> MastBranches { get; set; }

    public virtual DbSet<MastComp> MastComps { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    public virtual DbSet<WarehouseAreaMaster> WarehouseAreaMasters { get; set; }

    public virtual DbSet<WarehouseLocationMaster> WarehouseLocationMasters { get; set; }

    public virtual DbSet<WarehouseRackMaster> WarehouseRackMasters { get; set; }

    public virtual DbSet<WarehouseShelfMaster> WarehouseShelfMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-P99NB78;Database=Inventory;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinancialYear>(entity =>
        {
            entity.HasKey(e => e.FinanYearId).HasName("PK_Financial Year");

            entity.ToTable("FinancialYear");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.FinancialYearFrom)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.FinancialYearName)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.FinancialYearTo)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.HasOne(d => d.Comp).WithMany(p => p.FinancialYears)
                .HasForeignKey(d => d.CompId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FinanCompId");
        });

        modelBuilder.Entity<ItemCatagory>(entity =>
        {
            entity.HasKey(e => e.ItemCatId);

            entity.ToTable("Item Catagory");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.ItemCatagoryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Item Catagory Name");
        });

        modelBuilder.Entity<ItemCompany>(entity =>
        {
            entity.HasKey(e => e.ItemComId);

            entity.ToTable("ItemCompany");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation Date Time");
            entity.Property(e => e.ItemCompanyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Item Company Name");
        });

        modelBuilder.Entity<ItemSubCatagory>(entity =>
        {
            entity.HasKey(e => e.ItemSubCatId);

            entity.ToTable("ItemSubCatagory");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.SubCatagoryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Sub Catagory Name");
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("ItemType");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemUnit>(entity =>
        {
            entity.ToTable("ItemUnit");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.ItemUnitName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Item Unit Name");
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

            entity.HasOne(d => d.Comp).WithMany(p => p.MastBranches)
                .HasForeignKey(d => d.CompId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CompId");
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

        modelBuilder.Entity<WarehouseAreaMaster>(entity =>
        {
            entity.HasKey(e => e.WarehouseAreaId);

            entity.ToTable("Warehouse Area Master");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.WarehouseAreaName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Warehouse Area Name");
        });

        modelBuilder.Entity<WarehouseLocationMaster>(entity =>
        {
            entity.HasKey(e => e.WarehouseLocId);

            entity.ToTable("Warehouse Location Master");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.WarehouseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Warehouse Name");
        });

        modelBuilder.Entity<WarehouseRackMaster>(entity =>
        {
            entity.HasKey(e => e.WarehouseRackId);

            entity.ToTable("Warehouse Rack Master");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.WarehouseRackName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Warehouse Rack Name");
        });

        modelBuilder.Entity<WarehouseShelfMaster>(entity =>
        {
            entity.HasKey(e => e.WarehouseShelfId);

            entity.ToTable("Warehouse Shelf Master");

            entity.Property(e => e.CreatedBy).HasColumnName("Created By");
            entity.Property(e => e.CreationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Creation DateTime");
            entity.Property(e => e.WarehouseShelfName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Warehouse Shelf Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
