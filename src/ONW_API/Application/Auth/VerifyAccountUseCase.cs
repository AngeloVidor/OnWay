using ONW_API.Domain.Repositories;
using OnWay.API.Domain.Entities;

namespace OnWay.Application.VerifyAccount;

public sealed class VerifyAccountUseCase
{
    private readonly ITransporterRepository _transporterRepository;
    private readonly ITransporterVerificationRepository _verificationRepository;

    public VerifyAccountUseCase(
        ITransporterRepository transporterRepository,
        ITransporterVerificationRepository verificationRepository)
    {
        _transporterRepository = transporterRepository;
        _verificationRepository = verificationRepository;
    }

    public async Task ExecuteAsync(VerifyAccountCommand command)
    {
        var verification = await _verificationRepository.GetByCodeAsync(command.Code);

        if (verification is null || verification.IsExpired())
            throw new InvalidOperationException("Código inválido ou expirado");

        var transporter = Transporter.Create(
            verification.Name,
            verification.Email,
            verification.Phone,
            verification.Password
        );

        await _transporterRepository.AddAsync(transporter);
        await _verificationRepository.RemoveAsync(verification);

        await _transporterRepository.SaveChangesAsync();
        await _verificationRepository.SaveChangesAsync();
    }
}



