using VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;
using VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;
using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class Mapper : IMapper
{
    public Lazy<IReportMapper<LikeReportDto, LikeReport>> LikeReportMapper => new(() => new LikeReportMapper());

    public Lazy<IReportMapper<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}