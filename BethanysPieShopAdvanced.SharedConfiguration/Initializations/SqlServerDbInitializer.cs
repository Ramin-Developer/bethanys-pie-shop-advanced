namespace BethanysPieShop.SharedConfiguration.Initializations;

public class SqlServerDbInitializer(PieShopDbContext dbContext)
{
    public void Initialize()
    {
        // Ensure the database is up to date with the latest migrations
        _dbContext.Database.Migrate();

        // Seed the database only if it's empty
        if (DatabaseIsSeeded == false)
            SeedDatabase();
    }

    private void SeedDatabase()
    {
        // Perform migrations if targeting SQL Server
        _dbContext.Database.Migrate();

        // Seed categories
        if (_dbContext.Categories.Any() == false)
            _dbContext.Categories.AddRange(CategoryList);

        // Seed pies
        if (_dbContext.Pies.Any() == false)
            _dbContext.Pies.AddRange(GetPies);

        // Seed pies
        if (_dbContext.Orders.Any() == false)
            _dbContext.Orders.AddRange(GetOrders);

        _dbContext.SaveChanges();
    }

    private List<Category> CategoryList =>
        GetCategoryDict.Select(c => c.Value).ToList();

    private Dictionary<string, Category> GetCategoryDict
    {
        get
        {
            if (_categories == null)
            {
                var categoryList = new Category[]
                {
                    new()
                    {
                        Name = CategoryValues.FruitPies,
                        Description = CategoryValues.FruitPiesDecription,
                        DateAdded = DateTime.Today
                    },
                    new()
                    {
                        Name = CategoryValues.CheeseCakes,
                        Description = CategoryValues.CheeseCakesDecription,
                        DateAdded = DateTime.Today
                    },
                    new()
                    {
                        Name = CategoryValues.SeasonalPies,
                        Description = CategoryValues.SeasonalPiesDecription,
                        DateAdded = DateTime.Today
                    }
                };

                _categories = categoryList.ToDictionary(c => c.Name);
            }

            return _categories;
        }
    }

    private bool DatabaseIsSeeded => _dbContext.Pies.Any();

    private List<Pie> GetPies =>
    [
        new Pie
        {
            Name = PieValues.CaramelPopcornCheeseCake,
            Price = 263.92M,
            ShortDescription = PieValues.CaramelPopcornCheeseCakeShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 2,
            InStock = true,
            IsPieOfTheWeek = true,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.CaramelPopcornCheeseCakeImage,
            ImageThumbnailUrl = PieValues.CaramelPopcornCheeseCakeImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.ChocolateCheeseCake,
            Price = 229.43M,
            ShortDescription = PieValues.ChocolateCheeseCakeShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 2,
            InStock = true,
            IsPieOfTheWeek = true,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.ChocolateCheeseCakeImage,
            ImageThumbnailUrl = PieValues.ChocolateCheeseCakeImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.PistacheCheeseCake,
            Price = 252.43M,
            ShortDescription = PieValues.PistacheCheeseCakeShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 2,
            InStock = true,
            IsPieOfTheWeek = true,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.PistacheCheeseCakeImage,
            ImageThumbnailUrl = PieValues.PistacheCheeseCakeImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.PecanPie,
            Price = 252.43M,
            ShortDescription = PieValues.PecanPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 1,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.PecanPieImage,
            ImageThumbnailUrl = PieValues.PecanPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.BirthdayPie,
            Price = 344.43M,
            ShortDescription = PieValues.BirthdayPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 3,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.BirthdayPieImage,
            ImageThumbnailUrl = PieValues.BirthdayPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.ApplePie,
            Price = 148.93M,
            ShortDescription = PieValues.ApplePieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 1,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.ApplePieImage,
            ImageThumbnailUrl = PieValues.ApplePieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.BlueBerryCheeseCake,
            Price = 217.93M,
            ShortDescription = PieValues.BlueBerryCheeseCakeShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 2,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.BlueBerryCheeseCakeImage,
            ImageThumbnailUrl = PieValues.BlueBerryCheeseCakeImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.CheeseCake,
            Price = 217.93M,
            ShortDescription = PieValues.CheeseCakeShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 2,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.CheeseCakeImage,
            ImageThumbnailUrl = PieValues.CheeseCakeImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.CherryPie,
            Price = 183.43M,
            ShortDescription = PieValues.CherryPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 1,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.CherryPieImage,
            ImageThumbnailUrl = PieValues.CherryPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.ChristmasApplePie,
            Price = 163.43M,
            ShortDescription = PieValues.ChristmasApplePieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 3,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.ChristmasApplePieImage,
            ImageThumbnailUrl = PieValues.ChristmasApplePieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.CranberryPie,
            Price = 206.43M,
            ShortDescription = PieValues.CranberryPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 3,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.CranberryPieImage,
            ImageThumbnailUrl = PieValues.CranberryPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.PeachPie,
            Price = 183.43M,
            ShortDescription = PieValues.PeachPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 1,
            InStock = false,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.PeachPieImage,
            ImageThumbnailUrl = PieValues.PeachPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.PumpkinPie,
            Price = 148.93M,
            ShortDescription = PieValues.PumpkinPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 3,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.PumpkinPieImage,
            ImageThumbnailUrl = PieValues.PumpkinPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.RhubarbPie,
            Price = 183.43M,
            ShortDescription = PieValues.RhubarbPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 1,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.RhubarbPieImage,
            ImageThumbnailUrl = PieValues.RhubarbPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.StrawberryPie,
            Price = 183.43M,
            ShortDescription = PieValues.StrawberryPieShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 1,
            InStock = true,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.StrawberryPieImage,
            ImageThumbnailUrl = PieValues.StrawberryPieImageThumbnail,
        },
        new Pie
        {
            Name = PieValues.StrawberryCheeseCake,
            Price = 217.93M,
            ShortDescription = PieValues.StrawberryCheeseCakeShortDesc,
            LongDescription = PieValues.LongDescription,
            CategoryId = 2,
            InStock = false,
            IsPieOfTheWeek = false,
            AllergyInformation = string.Empty,
            ImageUrl = PieValues.StrawberryCheeseCakeImage,
            ImageThumbnailUrl = PieValues.StrawberryCheeseCakeImageThumbnail,
        }
    ];

    private List<Order> GetOrders =>
    [
        new Order()
        {
            FirstName = "Gill",
            LastName = "Cleeren",
            AddressLine1 = "Some street 123",
            City = "Brussels",
            Country = "Belgium",
            Email = "test@test.com",
            PhoneNumber = "555-123456",
            State = "NA",
            ZipCode = "1111",
            OrderPlaced = DateTime.Now,
            OrderStatus = OrderStatus.OutForDelivery,
            OrderTotal = 1235,
            OrderLines = new List<OrderLine>()
            {
                new()
                {
                    Amount = 1,
                    PieId = 1,
                    Price = 22.95M
                }
            }
        },
    ];

    private Dictionary<string, Category>? _categories;

    private readonly PieShopDbContext _dbContext = dbContext
        ?? throw new ArgumentException(GeneralValues.ArgumentNullError, nameof(dbContext));
}
