using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;

public class ParticipantReportElementDto : ReportElementBaseDto
{
    public ParticipantReportElementDto(ParticipantReportElementBuilder builder) : base(builder)
    {
        Type = builder.Type;
        NewName = builder.NewName;
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
}