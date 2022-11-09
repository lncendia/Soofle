using Microsoft.AspNetCore.Identity;

namespace VkQ.Application.Services.Services.Authentication.Entities;

public sealed class UserData : IdentityUser
{
    public UserData(string email)
    {
        Email = email;
        UserName = email;
    }
}