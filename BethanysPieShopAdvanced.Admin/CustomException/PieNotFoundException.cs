namespace BethanysPieShop.Admin.CustomException;

public class PieNotFoundException(int pieId, Exception innerException = null!)
    : CustomHandledException(PieValues.NotFoundIdError, PieValues.NotFoundIdError, innerException)
{
    public int PieId { get; set; } = pieId;
}
