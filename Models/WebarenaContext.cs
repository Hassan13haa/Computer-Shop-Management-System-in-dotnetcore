using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RenewApplication.Models;

public partial class WebarenaContext : DbContext
{
    public WebarenaContext()
    {
    }

    public WebarenaContext(DbContextOptions<WebarenaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Bill1> Bills1 { get; set; }

    public virtual DbSet<BillDet> BillDets { get; set; }

    public virtual DbSet<BillsRep> BillsReps { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("server=DESKTOP-0AJ27PB\\SQLEXPRESS; database=Webarena; TrustserverCertificate=True; trusted_connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__bill__D706DDB308A693AB");

            entity.ToTable("bill");

            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.BDate)
                .HasColumnType("datetime")
                .HasColumnName("b_date");
            entity.Property(e => e.CPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("c_phone");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Discount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.Due)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("due");
            entity.Property(e => e.NetTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("net_total");
            entity.Property(e => e.Paid)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("paid");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bills)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Bill_Customer");
        });

        modelBuilder.Entity<Bill1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("bills");

            entity.Property(e => e.BDate).HasColumnName("b_date");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Due).HasColumnName("due");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.NetTotal).HasColumnName("net_total");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
        });

        modelBuilder.Entity<BillDet>(entity =>
        {
            entity.HasKey(e => e.BdId).HasName("PK__bill_det__EC38960AD103E02F");

            entity.ToTable("bill_det");

            entity.Property(e => e.BdId).HasColumnName("bd_id");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.IId).HasColumnName("i_id");
            entity.Property(e => e.IName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("i_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SubTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("sub_total");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillDets)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("FK_bill_det_bill");
        });

        modelBuilder.Entity<BillsRep>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("bills_rep");

            entity.Property(e => e.BDate).HasColumnName("b_date");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Due).HasColumnName("due");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.NetTotal).HasColumnName("net_total");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Addres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("addres");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Bill).WithMany(p => p.Customers)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("fk_bill_id");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IId);

            entity.ToTable("items");

            entity.Property(e => e.IId).HasColumnName("i_id");
            entity.Property(e => e.Gen)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("gen");
            entity.Property(e => e.Harddisk)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("harddisk");
            entity.Property(e => e.Itemname)
                .HasMaxLength(50)
                .HasColumnName("itemname");
            entity.Property(e => e.Purchprice)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("purchprice");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Ram)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ram");
            entity.Property(e => e.Sellprice)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("sellprice");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("logins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.RepId).HasName("PK__repair__DC905AF408636631");

            entity.ToTable("repair");

            entity.Property(e => e.RepId).HasColumnName("rep_id");
            entity.Property(e => e.CustName)
                .HasMaxLength(50)
                .HasColumnName("cust_name");
            entity.Property(e => e.Descp)
                .HasMaxLength(70)
                .HasColumnName("descp");
            entity.Property(e => e.Item)
                .HasMaxLength(50)
                .HasColumnName("item");
            entity.Property(e => e.RepCost).HasColumnName("rep_cost");
            entity.Property(e => e.RepDate).HasColumnName("rep_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
