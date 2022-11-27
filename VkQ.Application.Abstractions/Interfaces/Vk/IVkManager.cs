﻿namespace VkQ.Application.Abstractions.Interfaces.Vk;

public interface IVkManager
{
    public Task SetVkAsync(Guid userId, string username, string password);
    public Task ActivateVkAsync(Guid userId, string? code);
}