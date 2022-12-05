using Microsoft.AspNetCore.Identity;

namespace VkQ.Application.Abstractions.Users.Entities;

public class RoleData : IdentityRole
{
    public RoleData(string name) : base(name)
    {
    }
}