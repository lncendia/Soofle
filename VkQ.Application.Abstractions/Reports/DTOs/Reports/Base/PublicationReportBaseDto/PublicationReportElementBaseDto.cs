using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

public abstract class PublicationReportElementBaseDto : ReportElementBaseDto
{
    protected PublicationReportElementBaseDto(PublicationReportElementBaseBuilder builder) : base(builder)
    {
        LikeChatName = builder.LikeChatName ?? throw new ArgumentNullException(nameof(builder.LikeChatName));
        ParticipantId = builder.ParticipantId ?? throw new ArgumentNullException(nameof(builder.ParticipantId));
        IsAccepted = builder.IsAccepted;
    }

    public string LikeChatName { get; }
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; }
}