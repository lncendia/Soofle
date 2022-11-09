namespace VkQ.Application.Abstractions.Interfaces.UsersAuthentication;

public interface IEmailService
{
    public Task SendEmailAsync(string email, string message);
}