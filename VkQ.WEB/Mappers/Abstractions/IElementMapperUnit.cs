using VkQ.Application.Abstractions.Elements.DTOs.ElementDto;
using VkQ.WEB.ViewModels.Elements.Base;

namespace VkQ.WEB.Mappers.Abstractions;

public interface IElementMapperUnit<in TElement, out TViewModel> where TElement : ElementDto
    where TViewModel : ElementViewModel
{
    TViewModel Map(TElement element);
}