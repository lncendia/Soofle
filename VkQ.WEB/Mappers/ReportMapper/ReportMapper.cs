using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Mappers.ReportMapper;

public class ReportMapper : IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReportViewModel>> LikeReportMapper =>
        new(() => new LikeReportMapper());

    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}