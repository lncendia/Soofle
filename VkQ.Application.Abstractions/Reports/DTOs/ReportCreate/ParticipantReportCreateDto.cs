namespace VkQ.Application.Abstractions.Reports.DTOs.ReportCreate;

public class ParticipantReportCreateDto
{
    public ParticipantReportCreateDto(Guid userId, long vkId)
    {
        UserId = userId;
        VkId = vkId;
    }

    public Guid UserId { get; }
    public long VkId { get; }
}