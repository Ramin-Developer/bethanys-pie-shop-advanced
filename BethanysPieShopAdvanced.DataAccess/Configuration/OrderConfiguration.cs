namespace BethanysPieShop.DataAccess.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // FirstName
        builder
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        // LastName
        builder
            .Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        // AddressLine1
        builder
            .Property(p => p.AddressLine1)
            .IsRequired()
            .HasMaxLength(100);

        // AddressLine2
        builder
            .Property(p => p.AddressLine2)
            .HasMaxLength(100);

        // ZipCode
        builder
            .Property(p => p.ZipCode)
            .IsRequired()
            .HasMaxLength(10);

        // City
        builder
            .Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100);

        // State
        builder
            .Property(p => p.State)
            .IsRequired()
            .HasMaxLength(100);

        // Country
        builder
            .Property(p => p.Country)
            .IsRequired()
            .HasMaxLength(100);

        // PhoneNumber
        builder
            .Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(25);

        // Email
        builder
            .Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

        // OrderTotal
        builder
            .Property(p => p.OrderTotal)
            .HasColumnType("decimal(18, 2)");

        // DatePlaced
        builder
            .Property(b => b.OrderPlaced)
            .IsRequired();
    }
}
