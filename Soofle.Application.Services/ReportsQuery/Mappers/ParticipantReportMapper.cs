using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.ReportsQuery.Mappers;

public class ParticipantReportMapper : IReportMapperUnit<ParticipantReportDto, ParticipantReport>
{
    public ParticipantReportDto Map(ParticipantReport report)
    {
        var builder = (ParticipantReportBuilder)ParticipantReportBuilder.ParticipantReportDto()
            .WithVk(report.VkId)
            .WithId(report.Id)
            .WithCreationDate(report.CreationDate)
            .WithElements(report.Participants.Count);
        if (!report.IsStarted) return builder.Build();
        builder.WithDates(report.StartDate!.Value, report.EndDate)
            .WithStatus(report.IsCompleted, report.IsSucceeded);
        if (!string.IsNullOrEmpty(report.Message)) builder.WithMessage(report.Message);
        var elements = report.Participants;
        return builder.Build();
    }
}