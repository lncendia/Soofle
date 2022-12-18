using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Mappers.Abstractions;

public interface IReportMapper
{
    Lazy<IReportMapperUnit<LikeReportDto, LikeReportViewModel>> LikeReportMapper { get; }
    Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>> ParticipantReportMapper { get; }
}