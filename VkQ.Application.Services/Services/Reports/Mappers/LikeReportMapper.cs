using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class LikeReportMapper : IReportMapperUnit<LikeReportDto, LikeReport>
{
    public LikeReportDto Map(LikeReport report)
    {
        var builder = LikeReportBuilder.LikeReportDto();
        StaticMethods.ReportMapper.InitReportBuilder(builder, report);
        return builder.Build();
    }
}