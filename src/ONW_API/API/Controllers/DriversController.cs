// using System.Security.Claims;
// using Microsoft.AspNetCore.Mvc;
// using ONW_API.Application.Drivers;
// using ONW_API.Application.Security;

// namespace OnWay.API.Controllers;

// [ApiController]
// [Route("api/drivers")]
// public sealed class DriversController : ControllerBase
// {
//     private readonly CreateDriverUseCase _useCase;

//     public DriversController(CreateDriverUseCase useCase)
//     {
//         _useCase = useCase;
//     }

//     [HttpPost]
//     public async Task<IActionResult> Create([FromBody] CreateDriverCommand command)
//     {
//         var transporterId = ClaimsHelper.GetUserId(User);
//         if (transporterId == Guid.Empty)
//         {
//             return Unauthorized();
//         }
//         var driverId = await _useCase.ExecuteAsync(command, transporterId);

//         return CreatedAtAction(nameof(Create), new { id = driverId }, new { id = driverId });
//     }
// }
