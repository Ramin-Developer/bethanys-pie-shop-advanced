namespace BethanysPieShop.Admin.Controllers;

public class HomeController(ILogger<HomeController> logger, IPieModelErrorService errorService)
    : BaseController<HomeController>(logger, errorService)
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string message = null!, string details = null!) =>
        View(new ErrorViewModel
        {
            RequestId = Activity
                .Current?.Id
                ?? HttpContext.TraceIdentifier,

            ErrorMessage = message,
            Details = details
        });
}
