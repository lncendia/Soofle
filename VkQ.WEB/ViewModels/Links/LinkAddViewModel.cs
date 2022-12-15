using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Links;

public class LinkAddViewModel
{
    public LinkAddViewModel(string name1, string name2, Guid id)
    {
        Name1 = name1;
        Name2 = name2;
        Id = id;
    }

    public string Name1 { get; }
    public string Name2 { get;  }
    public Guid Id { get;  }
}