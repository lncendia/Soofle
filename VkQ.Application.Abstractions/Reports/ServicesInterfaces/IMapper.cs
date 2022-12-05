using VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;
using VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.Reports.ServicesInterfaces;

public interface IMapper
{
    public Lazy<IReportMapper<LikeReportDto, LikeReport>> LikeReportMapper { get; }
    public Lazy<IReportMapper<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper { get; }
}