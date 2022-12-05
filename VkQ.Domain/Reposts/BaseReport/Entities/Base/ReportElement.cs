using VkQ.Domain.Abstractions;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Base;

public abstract class ReportElement : Entity
{
    protected ReportElement(string name, long vkId, ReportElement? parent = null)
    {
        Name = name;
        VkId = vkId;
        Parent = parent;
    }
    public string Name { get; }
    public long VkId { get; }
    protected readonly ReportElement? Parent;
}