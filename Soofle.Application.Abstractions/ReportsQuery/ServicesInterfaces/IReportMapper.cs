using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReport>> LikeReportMapper { get; }
    public Lazy<IReportMapperUnit<CommentReportDto, CommentReport>> CommentReportMapper { get; }
    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper { get; }
}