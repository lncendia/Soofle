using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class ParticipantReportMapper : IReportMapperUnit<ParticipantReportDto, ParticipantReport>
{
    public ParticipantReportDto Map(ParticipantReport report)
    {
        var builder = (ParticipantReportBuilder)ParticipantReportBuilder.ParticipantReportDto()
            .WithVk(report.VkId)
            .WithId(report.Id)
            .WithCreationDate(report.CreationDate);
        if (!report.IsStarted) return builder.Build();
        builder.WithDates(report.StartDate!.Value, report.EndDate)
            .WithStatus(report.IsCompleted, report.IsSucceeded);
        if (!string.IsNullOrEmpty(report.Message)) builder.WithMessage(report.Message);
        var elements = report.Participants;
        return builder.Build();
    }
}