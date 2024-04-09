namespace BethanysPieShop.ApiControllers;

// Todo: Fix the issue of "CategoryName is required" when adding or updating a pie.
// Todo: In the Admin, when trying to delete a category an exception is thrown with the message:
//       "This page isn’t working right now. If the problem continues, contact the site owner."

[Route("api/[controller]")]
[ApiController]
[Produces(GeneralValues.JsonMediaType)]
public class PieController(ILogger<Pie> logger, IPieService pieService) : ControllerBase
{
    // Get: /api/pie
    [HttpGet]
    public async Task<ActionResult<List<PieDto>>> GetAll()
    {
        var pieDtoList = await _pieService
            .GetPiesAsync();

        return Ok(pieDtoList);
    }

    // Get: /api/pie/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PieDto>> GetPieByIdAsync(int id)
    {
        var pieDto = await _pieService
            .GetPieByIdAsync(id);

        return Ok(pieDto);
    }

    // Post: api/pie
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PieDto pieDto)
    {
        _ = await _pieService
            .AddPieAsync(pieDto);

        return Ok();
    }

    // Put: api/pie/5
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(int id, [FromBody] PieDto updatedPieDto)
    {
        if (id != updatedPieDto.Id)
            return BadRequest(PieValues.IdMismatchError);

        var affectedRows = await _pieService
                .UpdatePieAsync(updatedPieDto);

        if (affectedRows <= 0)
        {
            var errorMsg = PieValues.NotFoundIdError.Replace("{pieId}", updatedPieDto.Id.ToString());

            return NotFound(errorMsg);
        }

        return Ok(affectedRows);
    }

    // Delete: api/pie/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest(GeneralValues.InvalidIdError);

        var pieDto = await _pieService
            .GetPieByIdAsync(id);

        if (pieDto == null)
        {
            var errorMsg = PieValues.NotFoundIdError.Replace("{pieId}", id.ToString());

            return NotFound(errorMsg);
        }
        await _pieService.DeletePieAsync(id);

        return Ok();
    }

    private readonly ILogger<Pie> _logger = logger
            ?? throw new ArgumentNullException(nameof(logger), GeneralValues.ArgumentNullError);

    private readonly IPieService _pieService = pieService
        ?? throw new ArgumentNullException(nameof(pieService), GeneralValues.ArgumentNullError);
}
