namespace BethanysPieShop.Admin.CustomException;

public class PieNotFoundException : CustomHandledException
{
    public PieNotFoundException(int pieId, Exception innerException = null!)
        : base(PieValues.NotFoundIdError, PieValues.NotFoundIdError, innerException)
    {
        PieId = pieId;
    }

    public int PieId { get; set; }
}
