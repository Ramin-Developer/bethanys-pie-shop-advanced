
namespace BethanysPieShop.WebApi.Controllers;

// Todo: Fix the issue of "CategoryName is required" when adding or updating a pie.
// Todo: In the Admin: Under adding a new, choosing the category from the drop-down-menu doesn't have any effect.

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class PieController(ILogger<Pie> logger, IPieService pieService) : ControllerBase
{
    // Get: /api/pie
    [HttpGet("/api/pie")]
    public async Task<ActionResult<List<PieDto>>> GetAll()
    {
        var pieDtoList = await _pieService
            .GetPiesAsync();

        return Ok(pieDtoList);
    }

    // Get: api/pie/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PieDto>> GetById(int id)
    {
        var pieDto = await _pieService
                .GetPieByIdAsync(id);

        if (pieDto == null)
        {
            _logger.LogWarning(PieValues.NotFoundIdError);
            var warningMsg = PieValues.NotFoundIdError.Replace("{pieId}", id.ToString());

            return NotFound(warningMsg);
        }

        return Ok(pieDto);
    }

    // Post: api/pie
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] PieDto pieDto)
    {
        var existingPie = await _pieService
            .GetPieByNameAsync(pieDto.Name);

        if (existingPie != null)
        {
            _logger.LogWarning(PieValues.NotFoundIdError);
            var errorMsg = PieValues.FoundNameFormatError.Replace("{pieId}", pieDto.Name);

            return NotFound(errorMsg);
        }

        int affectedRows = await _pieService
            .AddPieAsync(pieDto);

        return Ok(affectedRows);
    }

    // Put: api/pie/5
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(int id, [FromBody] PieDto updatedPieDto)
    {
        if (id != updatedPieDto.Id)
        {
            return BadRequest(PieValues.IdMismatchError);
        }

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
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (id <= 0)
            return BadRequest(GeneralValues.IdInValidError);

        var affectedRows = await _pieService
                .DeletePieAsync(id);

        if (affectedRows == 0)
        {
            var errorMsg = PieValues.NotFoundIdError.Replace("{pieId}", id.ToString());

            return NotFound(errorMsg);
        }

        return Ok(affectedRows);
    }

    private readonly ILogger<Pie> _logger = logger
            ?? throw new ArgumentNullException(nameof(logger), GeneralValues.ArgumentNullError);
    
    private readonly IPieService _pieService = pieService
            ?? throw new ArgumentNullException(nameof(pieService), GeneralValues.ArgumentNullError);
}
