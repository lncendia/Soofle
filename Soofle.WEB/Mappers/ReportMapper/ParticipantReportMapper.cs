using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Reports;

namespace Soofle.WEB.Mappers.ReportMapper;

public class ParticipantReportMapper : IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>
{
    public ParticipantReportViewModel Map(ParticipantReportDto report)
    {
        return new ParticipantReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.VkId);
    }
}