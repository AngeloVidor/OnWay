using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ONW_API.Application.Packages
{
    public sealed class AddPackageUseCase
    {
        private readonly IPackageRepository _packageRepository;

        public AddPackageUseCase(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Package> ExecuteAsync(Guid shipmentId, Recipient recipient)
        {
            var nextNumber = await _packageRepository.GetNextTrackingNumberAsync(DateTime.UtcNow.Year);
            var package = new Package(shipmentId, recipient, () => nextNumber);

            await _packageRepository.AddAsync(package);
            await _packageRepository.SaveChangesAsync();

            return package;
        }
    }
}
