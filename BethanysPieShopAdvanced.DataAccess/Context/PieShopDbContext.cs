namespace BethanysPieShop.DataAccess.Context;

public class PieShopDbContext(DbContextOptions<PieShopDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Pie> Pies { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PieShopDbContext).Assembly);

        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Pie>().ToTable("Pies");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderLine>().ToTable("OrderLines");
    }
}
