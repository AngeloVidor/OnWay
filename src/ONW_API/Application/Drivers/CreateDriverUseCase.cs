using System;
using System.Threading.Tasks;
using OnWay.Domain.Transporters.ValueObjects;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.Entities;

namespace ONW_API.Application.Drivers
{
    public sealed class CreateDriverUseCase
    {
        private readonly IDriverRepository _repository;

        public CreateDriverUseCase(IDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<Driver> ExecuteAsync(Guid transporterId, string name, string phone)
        {
            var phoneNumber = new PhoneNumber(phone);
            var driver = Driver.Create(name, phoneNumber, transporterId);

            await _repository.AddAsync(driver);
            await _repository.SaveChangesAsync();

            return driver;
        }

    }
}
