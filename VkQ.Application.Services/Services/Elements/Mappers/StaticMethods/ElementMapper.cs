using VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementDto;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;

namespace VkQ.Application.Services.Services.Elements.Mappers.StaticMethods;

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