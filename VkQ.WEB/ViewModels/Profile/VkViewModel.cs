namespace VkQ.WEB.ViewModels.Profile;

public class VkViewModel
{
    public VkViewModel(string login, string password, bool isActive)
    {
        Login = login;
        int length = password.Length / 3;
        Password = password[..length];
        Password += new string('*', length);
        Password += password[^length..];
        IsActive = isActive;
    }

    public string Login { get; }
    public string Password { get; }
    public bool IsActive { get; }
}