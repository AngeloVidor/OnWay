using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using OnWay.API.Domain.Entities;
using OnWay.Application.Email;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Application.Transporters;

public sealed class CreateTransporterUseCase
{
    private readonly ITransporterVerificationRepository _verificationRepository;
    private readonly IEmailSender _emailSender;

    public CreateTransporterUseCase(
        ITransporterVerificationRepository verificationRepository,
        IEmailSender emailSender)
    {
        _verificationRepository = verificationRepository;
        _emailSender = emailSender;
    }

    public async Task ExecuteAsync(CreateTransporterCommand command)
    {
        var verification = TransporterVerification.Create(
            command.Name,
            Email.Create(command.Email),
            PhoneNumber.Create(command.Phone),
            Password.Create(command.Password)
        );

        await _verificationRepository.AddAsync(verification);
        await _verificationRepository.SaveChangesAsync();

        await _emailSender.SendAsync(
            verification.Email.Value,
            "Verificação de Conta - OnWay",
            $"Seu código de verificação é: {verification.Code}"
        );
    }
}


