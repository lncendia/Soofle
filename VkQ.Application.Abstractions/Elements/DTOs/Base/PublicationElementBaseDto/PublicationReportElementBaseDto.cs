namespace VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementBaseDto;

public abstract class PublicationReportElementBaseDto : ReportElementBaseDto
{
    protected PublicationReportElementBaseDto(PublicationReportElementBaseBuilder builder) : base(builder)
    {
        LikeChatName = builder.LikeChatName ?? throw new ArgumentNullException(nameof(builder.LikeChatName));
        ParticipantId = builder.ParticipantId ?? throw new ArgumentNullException(nameof(builder.ParticipantId));
        IsAccepted = builder.IsAccepted;
        Vip = builder.Vip;
    }

    public string LikeChatName { get; }
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; }
    public bool Vip { get; }
}