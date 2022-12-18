using VkQ.Domain.Abstractions;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Base;

public abstract class ReportElement : Entity
{
    protected ReportElement(string name, long vkId)
    {
        Name = name;
        VkId = vkId;
    }
    public string Name { get; }
    public long VkId { get; }
}