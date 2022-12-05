namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;

public abstract class ReportElementBaseBuilder
{
    public Guid? Id;
    public string? Name;
    public long? VkId;
    public Guid? Parent;

    
    public ReportElementBaseBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    public ReportElementBaseBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public ReportElementBaseBuilder WithVkId(long vkId)
    {
        VkId = vkId;
        return this;
    }

    public ReportElementBaseBuilder WithParent(Guid parent)
    {
        Parent = parent;
        return this;
    }
}