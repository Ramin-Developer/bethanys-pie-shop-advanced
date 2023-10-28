namespace BethanysPieShop.DataAccess.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Name
        builder
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Description
        builder
            .Property(b => b.Description)
            .HasMaxLength(1000);

        // DateAdded
        builder
            .Property(b => b.DateAdded)
            .IsRequired();
    }
}
