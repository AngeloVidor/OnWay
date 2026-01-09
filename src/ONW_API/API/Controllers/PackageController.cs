using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Packages;
using ONW_API.Domain.ValueObjects;
using System.Threading.Tasks;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackageController : ControllerBase
    {
        private readonly AddPackageUseCase _addPackageUseCase;

        public PackageController(AddPackageUseCase addPackageUseCase)
        {
            _addPackageUseCase = addPackageUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody] AddPackageCommand command)
        {
            var recipient = new Recipient(
                command.RecipientName,
                command.RecipientEmail,
                command.RecipientPhone,
                command.RecipientAddress
            );

            var package = await _addPackageUseCase.ExecuteAsync(command.ShipmentId, recipient);

            return Ok(new
            {
                package.Id,
                package.ShipmentId,
                package.TrackingCode,
                Recipient = new
                {
                    recipient.Name,
                    recipient.Email,
                    recipient.Phone,
                    recipient.Address
                },
                package.Status
            });
        }

    }
}
