namespace BethanysPieShop.DataAccess.Configuration;

public class PieConfiguration : IEntityTypeConfiguration<Pie>
{
    public void Configure(EntityTypeBuilder<Pie> builder)
    {
        // Name
        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        // ShortDescription
        builder
            .Property(p => p.ShortDescription)
            .HasMaxLength(100);

        // LongDescription
        builder
            .Property(p => p.LongDescription)
            .HasMaxLength(1000);

        // AllergyInformation
        builder
            .Property(p => p.AllergyInformation)
            .HasMaxLength(1000);

        // Price
        builder
            .Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        // RowVersion
        builder
            .Property(p => p.RowVersion)
            .IsRowVersion();
    }
}
