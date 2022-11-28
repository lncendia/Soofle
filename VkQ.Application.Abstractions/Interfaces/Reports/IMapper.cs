using VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;
using VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.Interfaces.Reports;

public interface IMapper
{
    public Lazy<IReportMapper<LikeReportDto, LikeReport>> LikeReportMapper { get; }
    public Lazy<IReportMapper<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper { get; }
}