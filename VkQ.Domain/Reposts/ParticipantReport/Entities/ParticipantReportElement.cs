using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.ParticipantReport.DTOs;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReportElement : ReportElement
{
    public ParticipantReportElement(AddParticipantDto dto, IEnumerable<ParticipantReportElement>? children) :
        base(dto.Name, dto.VkId, children)
    {
        if (dto.Type == ElementType.Rename && string.IsNullOrEmpty(dto.OldName))
            throw new ArgumentException("OldName is required for rename element");
        Type = dto.Type;
        OldName = dto.OldName;
        Type = dto.Type;
    }

    public string? OldName { get; }
    public ElementType Type { get; }
}