﻿namespace VkQ.Application.Abstractions.DTO.Users;

public class VkLogoutDto
{
    public VkLogoutDto(string token, VkProxyDto proxy)
    {
        Proxy = proxy;
        Token = token;
    }

    public string Token { get; }
    public VkProxyDto Proxy { get; }
}