namespace Soofle.Application.Abstractions.Elements.DTOs.ElementDto;

public class ElementBuilder
{
    public string? Name { get; private set; }
    public long? VkId { get; private set; }

    public ElementBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public ElementBuilder WithVkId(long vkId)
    {
        VkId = vkId;
        return this;
    }
}