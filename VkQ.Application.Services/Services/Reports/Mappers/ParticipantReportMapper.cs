using VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;
using VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;
using VkQ.Application.Abstractions.Interfaces.Reports;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class ParticipantReportMapper : IReportMapper<ParticipantReportDto, ParticipantReport>
{
    public ParticipantReportDto Map(ParticipantReport report)
    {
        throw new NotImplementedException();
    }
}