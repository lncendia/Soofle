using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Mappers.ReportMapper;

public class ParticipantReportMapper : IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>
{
    public ParticipantReportViewModel Map(ParticipantReportDto report)
    {
        return new ParticipantReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.VkId);
    }
}