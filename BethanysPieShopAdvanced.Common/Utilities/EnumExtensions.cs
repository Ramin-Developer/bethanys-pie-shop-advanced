namespace BethanysPieShop.Common.Utilities;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        return value
            .GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName()
               ?? value.ToString();
    }

    public static TEnum GetCounterpart<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        if (enumValue is PieSortOption pieSortOption)
            return (TEnum)(object)GetPieSortOptionCounterpart(pieSortOption);

        var errorMsg = PieValues.NoCounterpartEnumTypeError.Replace("{enumType}", typeof(TEnum).Name);
        throw new InvalidOperationException(errorMsg);
    }

    private static PieSortOption GetPieSortOptionCounterpart(PieSortOption sortOption) =>
        sortOption switch
        {
            PieSortOption.IdAsc => PieSortOption.IdDesc,
            PieSortOption.IdDesc => PieSortOption.IdAsc,
            PieSortOption.CategoryIdAsc => PieSortOption.CategoryIdDesc,
            PieSortOption.CategoryIdDesc => PieSortOption.CategoryIdAsc,
            PieSortOption.NameAsc => PieSortOption.NameDesc,
            PieSortOption.NameDesc => PieSortOption.NameAsc,
            PieSortOption.CategoryNameAsc => PieSortOption.CategoryNameDesc,
            PieSortOption.CategoryNameDesc => PieSortOption.CategoryNameAsc,
            PieSortOption.PriceAsc => PieSortOption.PriceDesc,
            PieSortOption.PriceDesc => PieSortOption.PriceAsc,

            _ => throw new ArgumentOutOfRangeException(nameof(sortOption)),
        };
}
