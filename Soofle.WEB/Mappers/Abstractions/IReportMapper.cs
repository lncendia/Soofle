using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using Soofle.WEB.ViewModels.Reports;

namespace Soofle.WEB.Mappers.Abstractions;

public interface IReportMapper
{
    Lazy<IReportMapperUnit<LikeReportDto, LikeReportViewModel>> LikeReportMapper { get; }
    Lazy<IReportMapperUnit<CommentReportDto, CommentReportViewModel>> CommentReportMapper { get; }
    Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>> ParticipantReportMapper { get; }
}