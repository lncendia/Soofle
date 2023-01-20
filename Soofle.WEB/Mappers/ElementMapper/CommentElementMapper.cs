using Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Elements;

namespace Soofle.WEB.Mappers.ElementMapper;

public class CommentElementMapper : IElementMapperUnit<CommentElementDto, CommentElementViewModel>
{
    public CommentElementViewModel Map(CommentElementDto element) => MapRecursion(element);

    private static CommentElementViewModel MapRecursion(CommentElementDto dto) =>
        new(dto.Name, dto.VkId, dto.LikeChatName, dto.ParticipantId, dto.IsAccepted, dto.Vip, dto.Note,
            dto.Children.Select(MapRecursion),
            dto.Comments.Select(x => new CommentViewModel(x.PublicationId, x.Text, x.IsConfirmed)));
}