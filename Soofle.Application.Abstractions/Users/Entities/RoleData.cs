using Microsoft.AspNetCore.Identity;

namespace Soofle.Application.Abstractions.Users.Entities;

public class RoleData : IdentityRole
{
    public RoleData(string name) : base(name)
    {
    }
}