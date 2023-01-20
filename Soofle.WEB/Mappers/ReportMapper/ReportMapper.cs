using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Reports;

namespace Soofle.WEB.Mappers.ReportMapper;

public class ReportMapper : IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReportViewModel>> LikeReportMapper =>
        new(() => new LikeReportMapper());

    public Lazy<IReportMapperUnit<CommentReportDto, CommentReportViewModel>> CommentReportMapper =>
        new(() => new CommentReportMapper());

    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}