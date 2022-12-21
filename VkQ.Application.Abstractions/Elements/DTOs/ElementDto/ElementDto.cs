namespace VkQ.Application.Abstractions.Elements.DTOs.ElementDto;

public class ElementDto
{
    protected ElementDto(ElementBuilder builder)
    {
        Name = builder.Name ?? throw new ArgumentException("builder not formed", nameof(builder));
        VkId = builder.VkId ?? throw new ArgumentException("builder not formed", nameof(builder));
    }
    
    public string Name { get; }
    public long VkId { get; }
}