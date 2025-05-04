using System;
using Microsoft.EntityFrameworkCore;
using Domain.AggregateNodes;
using Domain.Aggregates.InvoiceAggregate;

namespace Infrastructure.DatabaseContexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tt;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Customer");

            entity.HasIndex(e => e.Email, "UQ_Customer_Email").IsUnique();
            entity.HasIndex(e => e.PhoneNumber, "UQ_Customer_PhoneNumber").IsUnique();

            entity.Property(e => e.Id).HasColumnName("CustomerId");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(100);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Invoice");

            entity.Property(e => e.Id).HasColumnName("InvoiceId");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.isPaid).HasColumnType("bit");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.ProductId }).HasName("PK_InvoiceItem");

            entity.ToTable("InvoiceItem");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceId");
            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.Quantity).HasColumnName("Quantity");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Product");

            entity.Property(e => e.Id).HasColumnName("ProductId");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
