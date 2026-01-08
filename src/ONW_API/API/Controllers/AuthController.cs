using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Auth;
using ONW_API.Application.Transporters;
using OnWay.Application.VerifyAccount;

namespace OnWay.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly LoginUseCase _useCase;
    private readonly VerifyAccountUseCase _verifyAccountUseCase;
    private readonly CreateTransporterUseCase _createTransporterUseCase;

    public AuthController(LoginUseCase useCase, VerifyAccountUseCase verifyAccountUseCase, CreateTransporterUseCase createTransporterUseCase)
    {
        _useCase = useCase;
        _verifyAccountUseCase = verifyAccountUseCase;
        _createTransporterUseCase = createTransporterUseCase;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] CreateTransporterCommand command)
    {
        await _createTransporterUseCase.ExecuteAsync(command);

        return Accepted();
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _useCase.ExecuteAsync(command);
        return Ok(result);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyAccount(
    [FromBody] VerifyAccountCommand command)
    {
        await _verifyAccountUseCase.ExecuteAsync(command);
        return NoContent();
    }
}
