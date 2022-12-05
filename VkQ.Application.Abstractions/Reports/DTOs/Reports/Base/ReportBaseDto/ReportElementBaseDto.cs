namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;

public abstract class ReportElementBaseDto
{
    protected ReportElementBaseDto(ReportElementBaseBuilder builder)
    {
        Id = builder.Id ?? throw new ArgumentNullException(nameof(builder.Id));
        Name = builder.Name ?? throw new ArgumentNullException(nameof(builder.Name));
        VkId = builder.VkId ?? throw new ArgumentNullException(nameof(builder.VkId));
        Parent = builder.Parent;
    }

    Guid Id { get; }
    public string Name { get; }
    public long VkId { get; }

    public Guid? Parent { get; }
}