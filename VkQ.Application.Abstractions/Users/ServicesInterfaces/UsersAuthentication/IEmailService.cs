namespace VkQ.Application.Abstractions.Users.ServicesInterfaces.UsersAuthentication;

public interface IEmailService
{
    public Task SendEmailAsync(string email, string message);
}