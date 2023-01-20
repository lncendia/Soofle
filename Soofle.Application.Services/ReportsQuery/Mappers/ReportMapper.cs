using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.ReportsQuery.Mappers;

public class ReportMapper : IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReport>> LikeReportMapper => new(() => new LikeReportMapper());

    public Lazy<IReportMapperUnit<CommentReportDto, CommentReport>> CommentReportMapper =>
        new(() => new CommentReportMapper());

    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}