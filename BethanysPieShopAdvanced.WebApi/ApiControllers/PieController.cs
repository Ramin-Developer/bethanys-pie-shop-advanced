namespace BethanysPieShop.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Produces(GeneralValues.JsonMediaType)]
public class PieController(ILogger<Pie> logger, IPieService pieService) : ControllerBase
{
    // Get: /api/pie
    [HttpGet]
    public async Task<ActionResult<List<PieDto>>> GetAllAsync()
    {
        var pieDtoList = await _pieService.GetPiesAsync();
        return Ok(pieDtoList);
    }

    // Get: /api/pie/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PieDto>> GetPieByIdAsync(int id)
    {
        var pieDto = await _pieService.GetPieByIdAsync(id);
        if (pieDto is null)
            return NotFound();
        return Ok(pieDto);
    }

    // Post: api/pie
    [HttpPost]
    public async Task<ActionResult<PieDto>> CreateAsync([FromBody] PieDto pieDto)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        try
        {
            var createdPie = await _pieService.AddPieAsync(pieDto);
            if (createdPie is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new pie record.");

            // Return 201 with location header pointing to GetPieByIdAsync
            return CreatedAtAction(nameof(GetPieByIdAsync), new { id = createdPie.Id }, createdPie);
        }
        catch (EntityNotFoundException<Pie> ex)
        {
            _logger.LogError(ex, "The pie was not found.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the pie.");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // Put: api/pie/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PieDto updatedPieDto)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        await _pieService.UpdatePieAsync(id, updatedPieDto);
        return NoContent();
    }

    // Delete: api/pie/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (id <= 0)
            return BadRequest(GeneralValues.InvalidIdError);

        var pieDto = await _pieService.GetPieByIdAsync(id);
        if (pieDto is null)
        {
            var errorMsg = PieValues.NotFoundIdError.Replace("{pieId}", id.ToString());
            return NotFound(errorMsg);
        }

        await _pieService.DeletePieAsync(id);
        return NoContent();
    }

    private readonly ILogger<Pie> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger), GeneralValues.ArgumentNullError);
    private readonly IPieService _pieService = pieService
        ?? throw new ArgumentNullException(nameof(pieService), GeneralValues.ArgumentNullError);
}
