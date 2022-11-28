namespace VkQ.Application.Abstractions.DTO.Reports.Base.ReportBaseDto;

public abstract class ReportElementBaseDto
{
    protected ReportElementBaseDto(ReportElementBaseBuilder builder)
    {
        Name = builder.Name ?? throw new ArgumentNullException(nameof(builder.Name));
        VkId = builder.VkId ?? throw new ArgumentNullException(nameof(builder.VkId));
        if (builder.Children != null) Children.AddRange(builder.Children);
    }
    public string Name { get; }
    public long VkId { get; }

    protected List<ReportElementBaseDto> Children = new();
}