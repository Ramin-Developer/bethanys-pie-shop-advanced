namespace BethanysPieShop.Admin.CustomExceptions;

public class NotFoundPieException(int pieId, Exception innerException = null!)
    : CustomHandledException(PieValues.NotFoundIdError, PieValues.NotFoundIdError, innerException)
{
    public int PieId { get; set; } = pieId;
}
