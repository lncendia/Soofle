using VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;
using VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;
using VkQ.Application.Abstractions.Interfaces.Reports;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class Mapper : IMapper
{
    public Lazy<IReportMapper<LikeReportDto, LikeReport>> LikeReportMapper => new(() => new LikeReportMapper());

    public Lazy<IReportMapper<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}