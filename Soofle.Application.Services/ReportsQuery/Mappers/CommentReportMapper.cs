using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using Soofle.Domain.Reposts.CommentReport.Entities;

namespace Soofle.Application.Services.ReportsQuery.Mappers;

public class CommentReportMapper : IReportMapperUnit<CommentReportDto, CommentReport>
{
    public CommentReportDto Map(CommentReport report)
    {
        var builder = CommentReportBuilder.CommentReportDto();
        StaticMethods.ReportMapper.InitReportBuilder(builder, report);
        var users = report.Elements.GroupBy(x => x.LikeChatName).Select(x => x.Key);
        builder.WithLinkedUsers(users).WithElements(report.Elements.Count);
        return builder.Build();
    }
}