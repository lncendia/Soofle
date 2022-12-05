using VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;
using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class ParticipantReportMapper : IReportMapper<ParticipantReportDto, ParticipantReport>
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
        if (elements.Any()) builder.WithReportElements(elements.Select(CreateElement));
        return builder.Build();
    }

    private static ParticipantReportElementDto CreateElement(ParticipantReportElement element)
    {
        var elementBuilder = ParticipantReportElementBuilder.ParticipantReportElementDto();
        elementBuilder
            .WithName(element.Name)
            .WithVkId(element.VkId);

        if (element.Type.HasValue) elementBuilder.WithType(element.Type.Value, element.NewName);
        if (element.Parent != null) elementBuilder.WithParent(element.Parent.Id);
        return elementBuilder.Build();
    }
}