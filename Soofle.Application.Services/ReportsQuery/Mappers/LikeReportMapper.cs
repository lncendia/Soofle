using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using Soofle.Domain.Reposts.LikeReport.Entities;

namespace Soofle.Application.Services.ReportsQuery.Mappers;

public class LikeReportMapper : IReportMapperUnit<LikeReportDto, LikeReport>
{
    public LikeReportDto Map(LikeReport report)
    {
        var builder = LikeReportBuilder.LikeReportDto();
        StaticMethods.ReportMapper.InitReportBuilder(builder, report);
        var users = report.Elements.GroupBy(x => x.LikeChatName).Select(x => x.Key);
        builder.WithLinkedUsers(users).WithElements(report.Elements.Count);
        return builder.Build();
    }
}