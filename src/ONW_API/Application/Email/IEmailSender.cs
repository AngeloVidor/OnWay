namespace OnWay.Application.Email;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body);
}
