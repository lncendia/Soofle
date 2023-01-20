using Soofle.Domain.Abstractions;

namespace Soofle.Domain.Reposts.BaseReport.Entities;

public abstract class ReportElement : Entity
{
    private protected ReportElement(string name, long vkId, int id) : base(id)
    {
        Name = name;
        VkId = vkId;
    }

    public string Name { get; }
    public long VkId { get; }
}