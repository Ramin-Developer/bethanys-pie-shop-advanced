namespace BethanysPieShop.DataAccess.Configurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder
            .Property(ol => ol.Price)
            .HasColumnType("decimal(18, 2)");
    }
}
