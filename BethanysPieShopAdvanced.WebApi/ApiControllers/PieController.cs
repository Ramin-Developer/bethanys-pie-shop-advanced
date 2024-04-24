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
        var pieDtoList = await _pieService
            .GetPiesAsync();

        return Ok(pieDtoList);
    }

    // Get: /api/pie/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PieDto>> GetPieByIdAsync(int id)
    {
        var pieDto = await _pieService
            .GetPieByIdAsync(id);

        return Ok(pieDto);
    }

    // Post: api/pie
    [HttpPost]
    public async Task<ActionResult<PieDto>> CreateAsync([FromBody] PieDto pieDto)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        var createdPie = await _pieService
            .AddPieAsync(pieDto);

        _logger.LogInformation("Creating at action with ID: {PieId}", createdPie.Id);

        return CreatedAtAction(nameof(GetPieByIdAsync), new { id = createdPie.Id }, createdPie);
        //return Ok(new { Url = Url.Action(nameof(GetPieByIdAsync), new { id = createdPie.Id }) });
    }

    // Put: api/pie/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] PieDto updatedPieDto)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        _ = await _pieService
            .UpdatePieAsync(id, updatedPieDto);

        return Ok();
    }

    // Delete: api/pie/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
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
