// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using ONW_API.Application.Shipment;

// namespace ONW_API.API.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class TrackController : ControllerBase
//     {
//         private readonly TrackShipmentUseCase _trackShipmentUseCase;

//         public TrackController(TrackShipmentUseCase trackShipmentUseCase)
//         {
//             _trackShipmentUseCase = trackShipmentUseCase;
//         }

//         [HttpGet("track/{trackingCode}")]
//         public async Task<IActionResult> TrackShipment(string trackingCode)
//         {
//             try
//             {
//                 var result = await _trackShipmentUseCase.ExecuteAsync(trackingCode);
//                 return Ok(result);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 return NotFound(new { message = ex.Message });
//             }
//         }
//     }
// }