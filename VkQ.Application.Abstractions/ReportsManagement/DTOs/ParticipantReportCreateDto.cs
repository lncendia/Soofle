namespace VkQ.Application.Abstractions.ReportsManagement.DTOs;

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