using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Drivers;
using ONW_API.Application.Security;

namespace OnWay.API.Controllers;

[ApiController]
[Route("api/drivers")]
[Authorize(Roles = "Transporter")]
public sealed class DriversController : ControllerBase
{
    private readonly CreateDriverUseCase _useCase;

    public DriversController(CreateDriverUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDriverCommand request)
    {
        var transporterId = ClaimsHelper.GetUserId(User);
        if (transporterId == Guid.Empty)
            return Unauthorized();

        try
        {
            var driver = await _useCase.ExecuteAsync(
                transporterId,
                request.Name,
                request.Phone
            );

            return Ok(driver);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
