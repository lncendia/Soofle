namespace VkQ.Application.Abstractions.Elements.DTOs.Base.ElementDto;

public class ElementBuilder
{
    public string? Name;
    public long? VkId;

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