using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.ViewModels.Elements;

namespace VkQ.WEB.Mappers.ElementMapper;

public class LikeElementMapper : IElementMapperUnit<LikeElementDto, LikeElementViewModel>
{
    public LikeElementViewModel Map(LikeElementDto element) => MapRecursion(element);

    private static LikeElementViewModel MapRecursion(LikeElementDto dto)
    {
        return new LikeElementViewModel(dto.Name, dto.VkId, dto.LikeChatName, dto.ParticipantId, dto.IsAccepted,
            dto.Vip, dto.Children.Select(MapRecursion),
            dto.Likes.Select(x => new LikeViewModel(x.PublicationId, x.IsLiked, x.IsLoaded)));
    }
}