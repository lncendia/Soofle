using Soofle.Application.Abstractions.Elements.DTOs.ElementDto;
using Soofle.Domain.Reposts.BaseReport.Entities;

namespace Soofle.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapperUnit<TReportElementDto, in TReportElement> where TReportElementDto : ElementDto
    where TReportElement : ReportElement
{
    public List<TReportElementDto> Map(IEnumerable<TReportElement> elements);
}