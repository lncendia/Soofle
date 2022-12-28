namespace VkQ.Application.Abstractions.Users.DTOs;

public class VkDto
{
    public VkDto(string login, string password, bool isActive)
    {
        Login = login;
        Password = password;
        IsActive = isActive;
    }

    public string Login { get; }
    public string Password { get; }
    public bool IsActive { get; }
}