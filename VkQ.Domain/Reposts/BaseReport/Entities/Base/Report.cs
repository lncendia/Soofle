using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Base;

public abstract class Report
{
    protected Report(User user)
    {
        if (!user.IsSubscribed) throw new UserSubscribeException();
        Id = Guid.NewGuid();
        UserId = user.Id;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsSucceeded { get; private set; }
    public string? Message { get; private set; }
    public bool IsStarted => StartDate.HasValue;

    public void Start() => StartDate = DateTimeOffset.Now;

    protected void Succeed()
    {
        ValidateFinish();
        EndDate = DateTimeOffset.Now;
        IsCompleted = true;
        IsSucceeded = true;
    }

    protected void Fail(string message)
    {
        ValidateFinish();
        EndDate = DateTimeOffset.Now;
        IsCompleted = true;
        IsSucceeded = false;
        Message = message;
    }

    private void ValidateFinish()
    {
        if (!IsStarted) throw new ReportNotStartedException();
        if (IsCompleted) throw new ReportAlreadyCompletedException();
    }
}