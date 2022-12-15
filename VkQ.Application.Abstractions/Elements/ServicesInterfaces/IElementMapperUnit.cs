using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapperUnit<TReportElementDto, TReportElement> where TReportElementDto : ReportElementBaseDto
    where TReportElement : ReportElement
{
    public List<TReportElementDto> Map(List<TReportElement> elements);
}