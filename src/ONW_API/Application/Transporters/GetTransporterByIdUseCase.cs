using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;
using OnWay.API.Domain.Entities;

namespace ONW_API.Application.Transporters
{
    public class GetTransporterByIdUseCase
    {
        private readonly ITransporterRepository _transporterRepository;

        public GetTransporterByIdUseCase(ITransporterRepository transporterRepository)
        {
            _transporterRepository = transporterRepository;
        }

        public async Task<Transporter?> ExecuteAsync(Guid transporterId)
        {
            return await _transporterRepository.GetByIdAsync(transporterId);
        }
    }
}