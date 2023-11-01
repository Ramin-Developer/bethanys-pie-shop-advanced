namespace BethanysPieShop.DataAccess.Configuration;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder
            .Property(ol => ol.Price)
            .HasColumnType("decimal(18, 2)");
    }
}
