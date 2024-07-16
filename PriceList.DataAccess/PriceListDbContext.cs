using Microsoft.EntityFrameworkCore;
using PriceList.DataAccess.Models;

namespace PriceList.DataAccess;

public class PriceListDbContext : DbContext
{
    public DbSet<Column> Columns { get; set; }

    public DbSet<DataType> DataTypes { get; set; }

    public DbSet<Models.PriceList> PriceLists { get; set; }

    public DbSet<PriceListColumn> PriceListColumns { get; set; }

    public DbSet<PriceListData> PriceListData { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<TextColumnData>TextColumnsData { get; set; }

    public DbSet<IntegerColumnData> IntegerColumnData { get; set; }

    public DbSet<DecimalColumnData> DecimalColumnData { get; set; }

    public PriceListDbContext(DbContextOptions<PriceListDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DataType>(b =>
        {
            b.Property(dt => dt.TypeName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Column>(b =>
        {
            b.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Models.PriceList>(b =>
        {
            b.Property(pl => pl.Name)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(pl => pl.CreationDate).IsRequired();
        });

        modelBuilder.Entity<PriceListColumn>(b =>
        {
            b.Property(pc => pc.ColumnId).IsRequired();
            b.Property(pc => pc.PriceListId).IsRequired();
            b.Property(pc => pc.DataTypeId).IsRequired();

            b.HasOne(pc => pc.PriceList)
                .WithMany(pl => pl.PriceListColumns)
                .HasForeignKey(c => c.PriceListId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(pc => pc.Column)
                .WithMany(c => c.PriceListColumns)
                .HasForeignKey(c => c.ColumnId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(pc => pc.DataType)
                .WithMany(c => c.PriceListColumns)
                .HasForeignKey(c => c.DataTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(b =>
        {
            b.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<PriceListData>(b =>
        {
            b.Property(pr => pr.PriceListColumnId).IsRequired();
            b.Property(pr => pr.ProductId).IsRequired();
            b.Property(pr => pr.RecordDate).IsRequired();

            b.HasOne(pc => pc.PriceListColumn)
                .WithMany(pl => pl.PriceListData)
                .HasForeignKey(c => c.PriceListColumnId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(pc => pc.Product)
                .WithMany(p => p.PriceListData)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TextColumnData>(b =>
        {
            b.Property(r => r.Id)
                .ValueGeneratedNever();

            b.Property(r => r.Value)
                .IsRequired()
                .HasMaxLength(200);

            b.HasOne(r => r.PriceListData)
                .WithOne(pr => pr.TexColumnData)
                .HasForeignKey<TextColumnData>(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DecimalColumnData>(b =>
        {
            b.Property(r => r.Id).ValueGeneratedNever();

            b.Property(r => r.Value)
                .HasColumnType("Decimal(10,2)")
                .IsRequired();

            b.HasOne(r => r.PriceListData)
                .WithOne(pr => pr.DecimalColumnData)
                .HasForeignKey<DecimalColumnData>(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<IntegerColumnData>(b =>
        {
            b.Property(r => r.Id).ValueGeneratedNever();
            b.Property(r => r.Value).IsRequired();

            b.HasOne(r => r.PriceListData)
                .WithOne(pr => pr.IntegerColumnData)
                .HasForeignKey<IntegerColumnData>(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}