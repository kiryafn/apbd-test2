using Microsoft.AspNetCore.Mvc;
using Test.Services.Abstractions;

namespace Test.Controllers;

[ApiController]
[Route("api/publishinghouses")]
public class PublishingHousesController : ControllerBase
{
    private readonly IPublishingHouseService _service;

    public PublishingHousesController(IPublishingHouseService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] string? country, [FromQuery] string? city)
    {
        var result = await _service.GetAllAsync(country, city);
        return Ok(result);
    }
}