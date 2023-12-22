namespace BethanysPieShop.IntegrationTest.Controllers;

public abstract class TestBase
{
    protected PieDto GetUpdatedPie(int pieId) =>
        new()
        {
            Id = pieId,
            Name = "Birthday Pie Upgraded",
            ShortDescription = "A Happy Birthday with This Pie!",
            LongDescription = PieValues.LongDescriptionValue,
            AllergyInformation = "No Allergy Reaction is expected",
            Price = 21.95m,
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypie.jpg",
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypiesmall.jpg",
            IsPieOfTheWeek = false,
            InStock = true,
            CategoryId = 1,
            CategoryName = "Fruit Pies",
        };

    protected PieDto GetCreatedPie(string pieName, int categoryId)
    {
        var pie = new PieDto()
        {
            Name = pieName,
            ShortDescription = "Carrot Pie",
            LongDescription = PieValues.LongDescriptionValue,
            AllergyInformation = "No Allergy Reaction is expected",
            Price = 21.95m,
            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypie.jpg",
            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypiesmall.jpg",
            IsPieOfTheWeek = false,
            InStock = true,
            CategoryId = categoryId,
            CategoryName = CategoryHelper.GetCategoryName(categoryId),
        };

        return pie;
    }
}
