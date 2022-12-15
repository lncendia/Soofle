using VkQ.Application.Abstractions.Elements.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class ParticipantReportElementMapper : IElementMapperUnit<ParticipantReportElementDto, ParticipantReportElement>
{
    public List<ParticipantReportElementDto> Map(List<ParticipantReportElement> elements)
    {
        var groupedElements = elements.GroupBy(x => x.Parent).ToList();

        return (from element in groupedElements.First(x => x.Key == null)
            let children = groupedElements.FirstOrDefault(x => x.Key == element)?.Select(x => Map(x, null))
            select Map(element, children)).ToList();
    }

    private static ParticipantReportElementDto Map(ParticipantReportElement element, IEnumerable<ParticipantReportElementDto>? children)
    {
        var elementBuilder = ParticipantReportElementBuilder.ParticipantReportElementDto();
        elementBuilder
            .WithName(element.Name)
            .WithVkId(element.VkId);

        if (element.Type.HasValue) elementBuilder.WithType(element.Type.Value, element.NewName);
        if (children != null) elementBuilder.WithChildren(children);
        return elementBuilder.Build();
    }
}