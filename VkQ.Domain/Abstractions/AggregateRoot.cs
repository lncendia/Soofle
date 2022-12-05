namespace VkQ.Domain.Abstractions;

public abstract class AggregateRoot
{
    public Guid Id { get; }
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot() => Id = Guid.NewGuid();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}