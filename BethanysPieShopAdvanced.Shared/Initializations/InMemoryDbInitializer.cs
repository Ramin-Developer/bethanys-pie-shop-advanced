namespace BethanysPieShop.Shared.Initializations;

public class InMemoryDbInitializer(PieShopDbContext dbContext)
{
    public void SeedInMemoryDb()
    {
        SeedCategories();
        SeedPies();
        SeedOrders();
    }

    public Dictionary<string, Category> GetCategoryDict =>
        new List<Category>
        {
            new()
            {
                Id = 1,
                Name = CategoryValues.FruitPies,
                Description = CategoryValues.FruitPiesDecription,
                DateAdded = DateTime.Today
            },
            new()
            {
                Id = 2,
                Name = CategoryValues.CheeseCakes,
                Description = CategoryValues.CheeseCakesDecription,
                DateAdded = DateTime.Today
            },
            new()
            {
                Id = 3,
                Name = CategoryValues.SeasonalPies,
                Description = CategoryValues.SeasonalPiesDecription,
                DateAdded = DateTime.Today
            }
        }.ToDictionary(c => c.Name);

    private void SeedCategories()
    {
        if (_dbContext.Categories.Any() == false)
        {
            _dbContext.Categories.AddRange(GetCategoryDict.Select(c => c.Value));
            _dbContext.SaveChanges();
        }
    }

    private void SeedPies()
    {
        // Prevent double seeding if DB wasn't recreated for any reason
        if (_dbContext.Pies.Any())
        {
            return;
        }

        _dbContext.AddRange(
            new Pie
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
                Id = 4,
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
                Id = 5,
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
                Id = 6,
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
                Id = 7,
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
                Id = 8,
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
                Id = 9,
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
                Id = 10,
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
                Id = 11,
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
                Id = 12,
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
                Id = 13,
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
                Id = 14,
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
                Id = 15,
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
                Id = 16,
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
            });

        _dbContext.SaveChanges();
    }

    private void SeedOrders()
    {
        // Prevent double seeding if DB wasn't recreated
        if (_dbContext.Orders.Any())
        {
            return;
        }

        _dbContext.Orders.AddRange(
            new Order()
            {
                Id = 1,
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
                OrderLines =
                [
                    new OrderLine()
                    {
                        Amount = 1,
                        PieId = 1,
                        Price = 22.95M
                    }
                ]
            });

        _dbContext.SaveChanges();
    }

    private readonly PieShopDbContext _dbContext = dbContext
        ?? throw new ArgumentException(GeneralValues.ArgumentNullError, nameof(dbContext));
}
