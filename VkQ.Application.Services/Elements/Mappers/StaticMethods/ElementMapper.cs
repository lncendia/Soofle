using VkQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;
using VkQ.Domain.Reposts.PublicationReport.Entities;

namespace VkQ.Application.Services.Elements.Mappers.StaticMethods;

internal class ElementMapper
{
    public static void InitElementBuilder(PublicationElementBuilder builder, PublicationReportElement element)
    {
        builder.WithAccepted(element.IsAccepted)
            .WithVip(element.Vip)
            .WithParticipantId(element.ParticipantId)
            .WithLikeChatName(element.LikeChatName)
            .WithName(element.Name)
            .WithVkId(element.VkId);
    }
}