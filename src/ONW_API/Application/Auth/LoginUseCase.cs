using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Application.Tokens;
using ONW_API.Domain.Repositories;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Application.Auth
{
    public sealed class LoginUseCase
    {
        private readonly ITransporterRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginUseCase(
            ITransporterRepository repository,
            ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<LoginResult> ExecuteAsync(LoginCommand command)
        {
            var email = Email.Create(command.Email);

            var transporter = await _repository.GetByEmailAsync(email);

            if (transporter is null)
                throw new InvalidOperationException("Credenciais inválidas");

            if (!transporter.Password.Verify(command.Password))
                throw new InvalidOperationException("Credenciais inválidas");

            var token = _tokenService.GenerateToken(transporter);

            return new LoginResult(token);
        }
    }
}