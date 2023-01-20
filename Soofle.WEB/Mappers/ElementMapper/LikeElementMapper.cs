using Soofle.Application.Abstractions.Elements.DTOs.LikeElementDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Elements;

namespace Soofle.WEB.Mappers.ElementMapper;

public class LikeElementMapper : IElementMapperUnit<LikeElementDto, LikeElementViewModel>
{
    public LikeElementViewModel Map(LikeElementDto element) => MapRecursion(element);

    private static LikeElementViewModel MapRecursion(LikeElementDto dto) =>
        new(dto.Name, dto.VkId, dto.LikeChatName, dto.ParticipantId, dto.IsAccepted, dto.Vip, dto.Note,
            dto.Children.Select(MapRecursion), dto.Likes.Select(x => new LikeViewModel(x.PublicationId, x.IsConfirmed)));
}