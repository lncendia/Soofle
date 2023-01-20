using Soofle.Application.Abstractions.Elements.DTOs.ElementDto;
using Soofle.WEB.ViewModels.Elements.Base;

namespace Soofle.WEB.Mappers.Abstractions;

public interface IElementMapperUnit<in TElement, out TViewModel> where TElement : ElementDto
    where TViewModel : ElementViewModel
{
    TViewModel Map(TElement element);
}