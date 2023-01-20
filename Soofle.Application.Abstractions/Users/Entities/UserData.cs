﻿using Microsoft.AspNetCore.Identity;

namespace Soofle.Application.Abstractions.Users.Entities;

public sealed class UserData : IdentityUser
{
    public UserData(string userName, string email)
    {
        Email = email;
        UserName = userName;
    }
}