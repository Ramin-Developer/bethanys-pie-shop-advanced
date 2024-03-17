namespace BethanysPieShop.SharedConfiguration.Initialization;

public class SqlServerDbInitializer(PieShopDbContext dbContext)
{
    public void Initialize()
    {
        // Ensure the database is up to date with the latest migrations
        _dbContext.Database.Migrate();

        // Seed the database only if it's empty
        if (DatabaseIsSeeded() == false)
            SeedDatabase();
    }

    private void SeedDatabase()
    {
        // Seed categories
        if (_dbContext.Categories.Any() == false)
            _dbContext.Categories.AddRange(CategoriesForSqlServer.Select(c => c.Value));

        // Seed pies
        if (_dbContext.Pies.Any() == false)
            _dbContext.Pies.AddRange(AllPies());

        _dbContext.SaveChanges();
    }

    private Dictionary<string, Category> CategoriesForSqlServer
    {
        get
        {
            if (_categories == null)
            {
                var categoryList = new Category[]
                {
                    new() { Name = "Fruit Pies", DateAdded = DateTime.Today },
                    new() { Name = "Cheese Cakes", DateAdded = DateTime.Today },
                    new() { Name = "Seasonal Pies", DateAdded = DateTime.Today }
                };

                _categories = [];

                foreach (var category in categoryList)
                    _categories.Add(category.Name, category);
            }

            return _categories;
        }
    }

    private bool DatabaseIsSeeded() => _dbContext.Pies.Any();

    private List<Pie> AllPies()
    {
        return
        [
            new Pie
            {
                Name = "Caramel Popcorn Cheese Cake",
            Price = 22.95M,
            ShortDescription = "The Ultimate Cheese Cake",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Cheese Cakes"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/caramelpopcorncheesecake.jpg",
            InStock = true,
            IsPieOfTheWeek = true,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/caramelpopcorncheesecakesmall.jpg",
            AllergyInformation = "",
            Ingredients = new List<Ingredient>
            {
                new(){ Name = "Sugar", Amount = "100 grams" },
                new(){ Name = "Fresh cream cheese", Amount = "300 grams" },
                new(){ Name = "Popcorn", Amount = "1 cup" },
            }
        },

        new Pie
        {
            Name = "Chocolate Cheese Cake",
            Price = 19.95M,
            ShortDescription = "The Chocolate Lover's Dream",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Cheese Cakes"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/chocolatecheesecake.jpg",
            InStock = true,
            IsPieOfTheWeek = true,
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/chocolatecheesecakesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Pistache Cheese Cake",
            Price = 21.95M,
            ShortDescription = "We're Going Nuts over This One",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Cheese Cakes"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/pistachecheesecake.jpg",
            InStock = true,
            IsPieOfTheWeek = true,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/pistachecheesecakesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Pecan Pie",
            Price = 21.95M,
            ShortDescription = "More Pecan than You Can Handle!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Fruit Pies"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/pecanpie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/pecanpiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Birthday Pie",
            Price = 29.95M,
            ShortDescription = "A Happy Birthday with This Pie!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Seasonal Pies"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Apple Pie",
            Price = 12.95M,
            ShortDescription = "Our Famous Apple Pies!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Fruit Pies"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Blueberry Cheese Cake",
            Price = 18.95M,
            ShortDescription = "You'll Love It!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Cheese Cakes"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/blueberrycheesecake.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/blueberrycheesecakesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Cheese Cake",
            Price = 18.95M,
            ShortDescription = "Plain Cheese Cake. Plain Pleasure.",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Cheese Cakes"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/cheesecake.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/cheesecakesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Cherry Pie",
            Price = 15.95M,
            ShortDescription = "A Summer Classic!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Fruit Pies"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Christmas Apple Pie",
            Price = 13.95M,
            ShortDescription = "Happy Holidays with This Pie!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Seasonal Pies"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Cranberry Pie",
            Price = 17.95M,
            ShortDescription = "A Christmas Favorite",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Seasonal Pies"],
            ImageUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Peach Pie",
            Price = 15.95M,
            ShortDescription = "Sweet as Peach",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Fruit Pies"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg",
            InStock = false,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Pumpkin Pie",
            Price = 12.95M,
            ShortDescription = "Our Halloween Favorite",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Seasonal Pies"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Rhubarb Pie",
            Price = 15.95M,
            ShortDescription = "My God, So Sweet!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Fruit Pies"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Strawberry Pie",
            Price = 15.95M,
            ShortDescription = "Our Delicious Strawberry Pie!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Fruit Pies"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg",
            InStock = true,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg",
            AllergyInformation = ""
        },

        new Pie
        {
            Name = "Strawberry Cheese Cake",
            Price = 18.95M,
            ShortDescription = "You'll Love It!",
            LongDescription = PieValues.LongDescriptionValue,
            Category = CategoriesForSqlServer["Cheese Cakes"],
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg",
            InStock = false,
            IsPieOfTheWeek = false,
            ImageThumbnailUrl =
                "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg",
            AllergyInformation = ""
        }
    ];
    }

    private void SeedOrders()
    {
        if (_dbContext.Orders.Any() == false)
        {
            _dbContext.Orders.AddRange(
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
                        new OrderLine()
                        {
                            Amount = 1,
                            PieId = 1,
                            Price = 22.95M
                        }
                    }
                });
        }

        _dbContext.SaveChanges();
    }

    private Dictionary<string, Category>? _categories;
    private readonly PieShopDbContext _dbContext = dbContext
            ?? throw new ArgumentException(GeneralValues.ArgumentNullError, nameof(dbContext));
}
