namespace VkQ.Domain.Reposts.BaseReport.Entities.Base;

public abstract class ReportElement : IEntity
{
    protected ReportElement(string name, long vkId, IEnumerable<ReportElement>? children)
    {
        Name = name;
        if (children != null) Children.AddRange(children);
        Id = Guid.NewGuid();
        VkId = vkId;
    }

    public Guid Id { get; }
    public string Name { get; }
    public long VkId { get; }
    protected readonly List<ReportElement> Children = new();
}