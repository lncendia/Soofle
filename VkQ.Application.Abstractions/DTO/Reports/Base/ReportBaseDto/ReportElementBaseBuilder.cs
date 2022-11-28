namespace VkQ.Application.Abstractions.DTO.Reports.Base.ReportBaseDto;

public abstract class ReportElementBaseBuilder
{
    public string? Name;
    public long? VkId;
    public IEnumerable<ReportElementBaseDto>? Children;

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

    protected ReportElementBaseBuilder WithChildren(IEnumerable<ReportElementBaseDto> children)
    {
        Children = children;
        return this;
    }
}