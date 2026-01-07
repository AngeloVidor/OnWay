using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;
using OnWay.API.Domain.Entities;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Application.Transporters
{
    public sealed class CreateTransporterUseCase
    {
        private readonly ITransporterRepository _repository;

        public CreateTransporterUseCase(ITransporterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> ExecuteAsync(CreateTransporterCommand command)
        {
            var email = Email.Create(command.Email);

            if (await _repository.EmailExistsAsync(email))
                throw new InvalidOperationException("Email já cadastrado");

            var transporter = Transporter.Create(
                command.Name,
                email,
                PhoneNumber.Create(command.Phone),
                Password.Create(command.Password)
            );

            await _repository.AddAsync(transporter);
            await _repository.SaveChangesAsync();

            return transporter.Id;
        }
    }
}