using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Application.Rersponses;
using ONW_API.Domain.Repositories;

namespace ONW_API.Application.Shipments
{
    public class GetShipmentPackagesUseCase
    {
        private readonly IPackageRepository _packageRepository;

        public GetShipmentPackagesUseCase(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<List<PackageResponse>> ExecuteAsync(Guid shipmentId)
        {
            var packages = await _packageRepository.GetByShipmentIdAsync(shipmentId);

            var response = packages.Select(pack => new PackageResponse
            {
                Id = pack.Id,
                ShipmentId = shipmentId,
                RecipientName = pack.Recipient.Name,
                RecipientPhone = pack.Recipient.Phone.Value,
                RecipientStreet = pack.Recipient.Address.Street,
                RecipientNumber = pack.Recipient.Address.Number,
                RecipientDistrict = pack.Recipient.Address.District,
                RecipientCity = pack.Recipient.Address.City,
                RecipientZipCode = pack.Recipient.Address.ZipCode
            }).ToList();

            return response;
        }
    }
}