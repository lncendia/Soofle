using Microsoft.AspNetCore.Identity;

namespace VkQ.Application.Abstractions.Entities;

public sealed class UserData : IdentityUser
{
    public UserData(string username, string email)
    {
        Email = email;
        UserName = username;
    }
}