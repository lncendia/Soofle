namespace Soofle.WEB.ViewModels.Elements.Base;

public abstract class ElementViewModel
{
    protected ElementViewModel(string name, long vkId)
    {
        Name = name;
        VkId = vkId;
    }

    public string Name { get; }
    public long VkId { get; }
}