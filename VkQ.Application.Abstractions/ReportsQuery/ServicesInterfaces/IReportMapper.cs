using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReport>> LikeReportMapper { get; }
    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper { get; }
}