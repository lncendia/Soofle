using VkQ.Application.Abstractions.Elements.DTOs.ElementDto;
using VkQ.Domain.Reposts.BaseReport.Entities;

namespace VkQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapperUnit<TReportElementDto, TReportElement> where TReportElementDto : ElementDto
    where TReportElement : ReportElement
{
    public List<TReportElementDto> Map(List<TReportElement> elements);
}