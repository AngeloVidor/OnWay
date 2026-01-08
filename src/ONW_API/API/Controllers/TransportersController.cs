using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Security;
using ONW_API.Application.Transporters;

namespace OnWay.API.Controllers;

[ApiController]
[Route("api/transporters")]
public sealed class TransportersController : ControllerBase
{
    private readonly CreateTransporterUseCase _useCase;
    private readonly GetTransporterByIdUseCase _getTransporterByIdUseCase;

    public TransportersController(CreateTransporterUseCase useCase, GetTransporterByIdUseCase getTransporterByIdUseCase)
    {
        _useCase = useCase;
        _getTransporterByIdUseCase = getTransporterByIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransporterCommand command)
    {
        await _useCase.ExecuteAsync(command);
        return Accepted();
    }

    [HttpGet("transporter")]
    public async Task<IActionResult> GetTransporter()
    {
        var transporterId = ClaimsHelper.GetUserId(User);

        if (transporterId == Guid.Empty)
            return Unauthorized();

        var transporter = await _getTransporterByIdUseCase.ExecuteAsync(transporterId);

        if (transporter is null)
            return NotFound();

        return Ok(transporter);
    }

}
