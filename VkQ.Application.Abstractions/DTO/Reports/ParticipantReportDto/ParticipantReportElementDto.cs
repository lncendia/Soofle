using VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;

public class ParticipantReportElementDto : PublicationReportElementBaseDto
{
    public ParticipantReportElementDto(ParticipantReportElementBuilder builder) : base(builder)
    {
        Type = builder.Type ?? throw new ArgumentNullException(nameof(builder.Type));
        OldName = builder.OldName;
    }

    public string? OldName { get; }
    public ElementType Type { get; }

    public new List<ParticipantReportElementDto> Children => base.Children.Cast<ParticipantReportElementDto>().ToList();
}