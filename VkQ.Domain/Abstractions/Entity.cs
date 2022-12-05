namespace VkQ.Domain.Abstractions;

public abstract class Entity
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id { get; }
}