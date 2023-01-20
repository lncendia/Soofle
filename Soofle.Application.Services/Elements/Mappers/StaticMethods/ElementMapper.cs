using Soofle.Application.Abstractions.Elements.DTOs.PublicationElementDto;
using Soofle.Domain.Reposts.PublicationReport.Entities;

namespace Soofle.Application.Services.Elements.Mappers.StaticMethods;

internal static class ElementMapper
{
    public static void InitElementBuilder(PublicationElementBuilder builder, PublicationReportElement element)
    {
        builder.WithAccepted(element.IsAccepted)
            .WithVip(element.Vip)
            .WithParticipantId(element.ParticipantId)
            .WithLikeChatName(element.LikeChatName)
            .WithName(element.Name)
            .WithVkId(element.VkId);
        if (element.Note != null) builder.WithNote(element.Note);
    }
}