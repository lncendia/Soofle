﻿using Soofle.Domain.Abstractions;
using Soofle.Domain.Reposts.BaseReport.Events;
using Soofle.Domain.Reposts.BaseReport.Exceptions;
using Soofle.Domain.Users.Entities;

namespace Soofle.Domain.Reposts.BaseReport.Entities;

public abstract class Report : AggregateRoot
{
    private protected Report(User user)
    {
        if (!user.IsSubscribed) throw new UserSubscribeException(user.Id, user.Name);
        if (!user.HasVk || !user.Vk!.IsActive) throw new UserVkException(user.Id);
        if (!user.Vk.ProxyId.HasValue) throw new UserVkException(user.Id);
        UserId = user.Id;
    }


    public Guid UserId { get; }
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public bool IsSucceeded { get; private set; }
    public string? Message { get; private set; }
    public bool IsStarted => StartDate.HasValue;
    public bool IsCompleted => EndDate.HasValue;

    protected readonly List<ReportElement> ReportElementsList = new();

    protected void Start() => StartDate = DateTimeOffset.Now;

    protected void Succeed()
    {
        EndDate = DateTimeOffset.Now;
        IsSucceeded = true;
        AddDomainEvent(new ReportFinishedEvent(Id, true, EndDate.Value));
    }

    protected void Fail(string message)
    {
        if (!IsStarted) StartDate = DateTimeOffset.Now;
        EndDate = DateTimeOffset.Now;
        IsSucceeded = false;
        Message = message;
        AddDomainEvent(new ReportFinishedEvent(Id, false, EndDate.Value));
    }
}