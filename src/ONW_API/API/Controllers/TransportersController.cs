using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Transporters;

namespace OnWay.API.Controllers;

[ApiController]
[Route("api/transporters")]
public sealed class TransportersController : ControllerBase
{
    private readonly CreateTransporterUseCase _useCase;

    public TransportersController(CreateTransporterUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransporterCommand command)
    {
        await _useCase.ExecuteAsync(command);
        return Accepted();
    }
}
