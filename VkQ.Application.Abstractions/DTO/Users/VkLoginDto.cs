﻿using VkQ.Application.Abstractions.DTO.Proxy;

namespace VkQ.Application.Abstractions.DTO.Users;

public class VkLoginDto
{
    public VkLoginDto(string login, string password, VkProxyDto proxy)
    {
        Login = login;
        Password = password;
        Proxy = proxy;
    }

    public string Login { get; }
    public string Password { get; }
    public VkProxyDto Proxy { get; }
}